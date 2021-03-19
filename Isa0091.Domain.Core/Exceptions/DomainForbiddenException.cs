using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Core.Exceptions
{
    public class DomainForbiddenException: Exception
    {
        public DomainForbiddenException(string message):base(message)
        {
            
        }

        public DomainForbiddenException()
        {

        }
    }
}
