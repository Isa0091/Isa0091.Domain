using Isa0091.Domain.Core.Exceptions;
using Isa0091.Domain.Mvc.Filters.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Mvc.Filters
{
    /// <summary>
    /// Transforma los errores <see cref="DomainBaseException"/> a un onjeto para desplegar como resultado del API
    /// </summary>
    public class ApiExceptionFilter: ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainForbiddenException)
            {
                context.Result = new ForbidResult();
            }

            if (context.Exception is DomainBaseException ex)
            {
                
                ApiExceptionResult result = new ApiExceptionResult()
                {
                    Message = ex.Message,
                    ErrorTypeCode = (int) ex.GetExceptionTypeCode(),
                    ErrorTypeName = ex.GetExceptionTypeDescription(),
                    RelatedField = ex.RelatedField,
                    RelatedObject = ex.RelatedObject.FullName
                };

                if (ex.GetErrorType() == HttpErrorType.Client)
                    context.Result = new BadRequestObjectResult(result);

                if (ex.GetErrorType() == HttpErrorType.NotFound)
                    context.Result = new NotFoundObjectResult(result);
            }
            
        }
    }
}
