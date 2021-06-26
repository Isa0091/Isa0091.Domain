using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Isa0091.Domain.Context.ServicesBusSenders;
using Isa0091.Domain.Core.Events;

namespace Isa0091.Domain.ContextInjection
{
    public static class Extensions
    {
        /// <summary>
        /// Agrega el db context yu registra todo lo necesair para poder despachar Integration Events
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceBusCnnString"></param>
        /// <param name="config"></param>
        /// <param name="modelsAssemblies">Los assenblies adonde estan ls modelss de lso cuales e pueden disprar eventyops</param>
        public static void AddServiceBusIntegrationEventSender(this IServiceCollection services,
            IntegrationEventTopicConfiguration config,
            params Assembly[] modelsAssemblies)
        {

            if(config != null)
            {
                List<Type> events = new List<Type>();
                foreach (Assembly modelAssembly in modelsAssemblies)
                {
                    events.AddRange(GetRootEntities(modelAssembly));
                }
                var adm = new Azure.Messaging.ServiceBus.Administration.ServiceBusAdministrationClient(config.ConnectionString);
                ServiceBusClient client = new ServiceBusClient(config.ConnectionString);
                foreach (Type root in events)
                {
                    var exits = adm.TopicExistsAsync(GetTopicName(root)).GetAwaiter().GetResult();
                    if (!exits)
                    {
                        var options = new CreateTopicOptions(GetTopicName(root));
                        options.DefaultMessageTimeToLive = new TimeSpan(0, 0, config.TimeToLiveSeconds);
                        options.EnablePartitioning = config.EnablePatitioning;
                        if (config.MaxSizeInMegabytes != null)
                            options.MaxSizeInMegabytes = config.MaxSizeInMegabytes.Value;

                        if (config.AutoDeleteSeconds != null)
                            options.AutoDeleteOnIdle = new TimeSpan(0, 0, config.AutoDeleteSeconds.Value);

                        if (config.DuplicateDetectionSeconds != null)
                        {
                            options.RequiresDuplicateDetection = true;
                            options.DuplicateDetectionHistoryTimeWindow =
                                new TimeSpan(0, 0, config.DuplicateDetectionSeconds.Value);
                        }

                        adm.CreateTopicAsync(options).GetAwaiter().GetResult();
                    }
                    var sender = client.CreateSender(GetTopicName(root));
                    services.AddSingleton(sender);

                }

                services.AddScoped<IIntegrationEventSender, ServiceBusTopicSender>();
            }

        }

        private static string GetTopicName(Type root)
        {
            return root.Name;
        }

        public static List<Type> GetRootEntities(Assembly assembly)
        {
            Type t = typeof(IIntegrationEvent);
            return assembly.GetTypes().Where(x=>t.IsAssignableFrom(x))
                .ToList();
        }
    }
}
