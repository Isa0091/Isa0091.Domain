using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Isa0091.Domain.Core.Events;

namespace Isa0091.Domain.Context.ServicesBusSenders
{
    public class ServiceBusTopicSender : IIntegrationEventSender
    {
        private readonly IEnumerable<ServiceBusSender> _senders;

        public ServiceBusTopicSender(IEnumerable<ServiceBusSender> senders)
        {
            _senders = senders;
        }
        public async Task SendIntegrationEvent(IIntegrationEvent evento)
        {
            var sender = _senders.SingleOrDefault(x => x.EntityPath == evento.GetType().Name);
            if (sender == null)
                throw new NotImplementedException(
                    $"No hay un sender implementado para la cola {evento.GetType().Name}");


            string jsonString = JsonSerializer.Serialize(evento, evento.GetType());
            byte[] objBytes = Encoding.UTF8.GetBytes(jsonString);
            ServiceBusMessage message = new ServiceBusMessage(new BinaryData(objBytes))
            {
                ContentType = "application/json"
            };

            if (evento is ISessionIntegrationEvent sessionEvent)
            {
                if (string.IsNullOrEmpty(sessionEvent.IdSessionId))
                    throw new InvalidOperationException(
                        $"Si el evento herada {nameof(ISessionIntegrationEvent)} es necesario especificar la session, si no dese sesiones use {nameof(IIntegrationEvent)}");
                message.SessionId = sessionEvent.IdSessionId;
            }
                


            await sender.SendMessageAsync(message);


        }
    }
}
