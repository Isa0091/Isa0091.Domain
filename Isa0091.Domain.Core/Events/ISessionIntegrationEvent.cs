using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Core.Events
{
    /// <summary>
    /// Interfaz utilizada si se desea que los mensajes de integracion manejen sesiones
    /// </summary>
    public interface ISessionIntegrationEvent : IIntegrationEvent
    {
        /// <summary>
        /// La cvadena de caracteres a utilizar par ala sesion
        /// </summary>
        string IdSessionId { get; set; }
    }
}
