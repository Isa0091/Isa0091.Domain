using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Isa0091.Domain.Core.Events;
using Isa0091.Domain.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Isa0091.Domain.Context
{
    public abstract class BaseContext<T> : Microsoft.EntityFrameworkCore.DbContext where T : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly IMediator _mediator;
        private readonly IIntegrationEventSender _senders;
        private readonly ILogger _loger;
        private const int ColasLogEventId = 3000;

        protected BaseContext(DbContextOptions<T> options, MediatR.IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected BaseContext(DbContextOptions<T> options, MediatR.IMediator mediator,IIntegrationEventSender sender, ILogger<T> logger) : base(options)
        {
            _mediator = mediator;
            _senders = sender;
            _loger = logger;
        }

        protected BaseContext(DbContextOptions<T> options, MediatR.IMediator mediator, IIntegrationEventSender sender) : base(options)
        {
            _mediator = mediator;
            _senders = sender;
            
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            List<RootEntity> rootEntities = GetRootEntities();
            rootEntities.ForEach(x=>x.IsValid());
            await SendDomainEvents(rootEntities, cancellationToken);
            
            int result = await base.SaveChangesAsync(cancellationToken);

            SendIntegrationEvents(rootEntities, cancellationToken);

            return result;
        }
        public override int SaveChanges()
        {
            throw new InvalidOperationException("No puede utiliza el metodo sincrono de save changes");
        }


        private async Task SendDomainEvents(List<RootEntity> rootEntities, CancellationToken cancellationToken)
        {
            foreach (RootEntity rootEntity in rootEntities.Where(x => x.GetEventos().Any()).ToList())
            {
                //Saco los integrations eventos que se pudieron haber generado
                foreach (INotification noti in rootEntity.GetEventos().OfType<IDomainEvent>())
                {
                    await _mediator.Publish(noti, cancellationToken);
                }
            }
        }

        private void SendIntegrationEvents(List<RootEntity> rootEntities, CancellationToken cancellationToken)
        {
            List<Task> colasTasks = new List<Task>();

            if (_senders != null)
            {
                foreach (RootEntity rootEntity in rootEntities.Where(x => x.GetEventos().Any()).ToList())
                {
                    //Saco los integrations eventos que se pudieron haber generado
                    foreach (IIntegrationEvent noti in rootEntity.GetEventos().OfType<IIntegrationEvent>())
                    {
                        colasTasks.Add(AgregarCola(noti, cancellationToken));
                    }
                }

                if (colasTasks.Count > 0)
                {
                    try
                    {
                        Task.WaitAll(colasTasks.ToArray());
                    }
                    catch (AggregateException ex)
                    {
                        if (_loger != null)
                        {
                            foreach (SendException exception in ex.InnerExceptions.OfType<SendException>())
                            {
                                _loger.LogCritical(ColasLogEventId, ex.InnerException, "Error enviando el mensaje {mensaje}", exception.EventObject);
                            }
                        }
                    }

                }
            }

           
        }


        private List<RootEntity> GetRootEntities()
        {
            //Saco una lista de los elementos agregados o modificados, que sean del tipo Entidad
            List<object> elementosCambiados = ChangeTracker.Entries()
                .Where(x => (x.Entity.GetType().IsSubclassOf(typeof(RootEntity)))).Select(x => x.Entity)
                .ToList();

            //Casteo la lista de objetos a root
            List<RootEntity> rootEntities = new List<RootEntity>();

            elementosCambiados.ForEach(x => rootEntities.Add((RootEntity)x));

            //Filtro para que solo tome las entidades que tienen integration Events
            return rootEntities.ToList();
        }

        private async Task AgregarCola(IIntegrationEvent integrationEvent, CancellationToken cancellationToken)
        {
            //TODO: ver que s elevante el logueo en caso d eerror
            try
            {
                await _senders.SendIntegrationEvent(integrationEvent);
            }
            catch (AggregateException ex)
            {
                throw new SendException(ex.Message, ex.Flatten().InnerException)
                {
                    EventObject = integrationEvent
                };
            }
        }

        internal class SendException : Exception
        {
            public SendException(string message, Exception innerException) : base(message, innerException)
            {
                
            }
            public IIntegrationEvent EventObject { get; set; }

        }
    }
}
