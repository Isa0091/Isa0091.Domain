using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Core.Exceptions
{
    /// <summary>
    /// Clase egnerada cuando se trata de acceder a un objeto que no existe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DomainNotFoundException<T> : DomainBaseException where T:Enum
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="relatedField"></param>
        /// <param name="relatedObject"></param>
        /// <param name="message"></param>
        protected DomainNotFoundException(T exceptionType, string relatedField, Type relatedObject, string message) : base(relatedField, relatedObject, message)
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
                $"El objeto de tipo {exceptionType.ToString()} referenciado por el campo {relatedField} del objeto {relatedObject.FullName} no existe";
        }


        public override HttpErrorType GetErrorType()
        {
            return HttpErrorType.NotFound;
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
