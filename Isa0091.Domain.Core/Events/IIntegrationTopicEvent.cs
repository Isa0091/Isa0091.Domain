using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Core.Events
{
    /// <summary>
    /// Representa un evento de integracion, es decir un evento que se maneja de manera externa al sistema,
    /// este incluye un metodo para sacr el nombre del topic a utilizar
    /// </summary>
    public interface IIntegrationTopicEvent : IIntegrationEvent
    {
        /// <summary>
        /// Saca el topic dond es eoclocara este evento
        /// </summary>
        /// <returns></returns>
        string GetTopicName();
    }
}
