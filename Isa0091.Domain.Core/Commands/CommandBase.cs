using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Isa0091.Domain.Core.Commands
{
    public abstract class CommandBase<TResult> : IRequest<TResult> where TResult : class
    {
    }

    public abstract class CommandBase : IRequest
    {
    }
}
