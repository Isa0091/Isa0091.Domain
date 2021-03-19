using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Isa0091.Domain.Core.Events
{
    /// <summary>
    /// Indica que un evento debe levantar un domian evemt
    /// </summary>
    public interface IDomainEvent : INotification, IEvent
    {
    }
}
