using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Isa0091.Domain.Core.Queries
{
    public class QueryBase<T> : IRequest<T> where T : class
    {
    }

    public class QueryBase : IRequest
    {
    }
}
