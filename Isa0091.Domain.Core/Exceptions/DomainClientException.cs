using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Core.Exceptions
{
    /// <summary>
    /// Excepcion cuando hay un error de datos cometido por el cliente
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DomainClientException<T> : DomainBaseException where T:Enum
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionType">El tipo de excepcion, ainformaciuxon debe servir para completar una frase como: "Error del cliente de tipo XXXXX en el campo Nombre"</param>
        /// <param name="relatedField">El campo relacionado al error, si es mas d euno se puede concatenar por "-"</param>
        /// <param name="relatedObject">El onbbjeto donde estaba el campo en el que ocurrio el error.</param>
        /// <param name="message"></param>
        protected DomainClientException(T exceptionType, string relatedField, Type relatedObject, string message) : base(relatedField, relatedObject, message)
        {
            ExceptionType = exceptionType;
        }

        /// <summary>
        /// El tipo de excepcion, ainformaciuxon debe servir para completar una frase como: "Error del cliente de tipo XXXXX en el campo Nombre"
        /// </summary>
        public T ExceptionType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="relatedField"></param>
        /// <param name="relatedObject"></param>
        /// <returns></returns>
        protected static string GetMessage(T exceptionType, string relatedField, Type relatedObject)
        {
            return
                $"Error de cliente de tipo {exceptionType.ToString()} en el campo {relatedField} del objeto {relatedObject.FullName}";
        }

        public override HttpErrorType GetErrorType()
        {
            return HttpErrorType.Client;
        }

        public override int GetExceptionTypeCode()
        {
            return Convert.ToInt32(ExceptionType);
        }

        public override string GetExceptionTypeDescription()
        {
            return ExceptionType.ToString();
        }
    }
}
