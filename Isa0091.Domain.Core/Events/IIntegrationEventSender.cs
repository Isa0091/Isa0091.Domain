using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Isa0091.Domain.Core.Events
{
    /// <summary>
    /// Permite despacher evento s deintegracion
    /// </summary>
    public interface IIntegrationEventSender
    {
        /// <summary>
        /// permite despachar el evento de integracion
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        Task SendIntegrationEvent(IIntegrationEvent evento);
    }
}
