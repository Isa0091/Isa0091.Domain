using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Core.Exceptions
{
    public abstract class DomainBaseException : Exception 
    {
        protected DomainBaseException(string relatedField, Type relatedObject, string message) : base(message)
        {
            RelatedField = relatedField;
            RelatedObject = relatedObject;
            
        }

        
        /// <summary>
        /// Tipos de errores HTTP mnejados
        /// </summary>
        /// <returns></returns>
        public abstract HttpErrorType GetErrorType();

        /// <summary>
        /// Ca,po relacionado al error
        /// </summary>
        public string RelatedField { get; set; }

        /// <summary>
        /// El obbjeto en el que ocurrio el error
        /// </summary>
        public Type RelatedObject { get; set; }

        /// <summary>
        /// El codigo del tipo de excepcion
        /// </summary>
        /// <returns></returns>
        public abstract int GetExceptionTypeCode();

        /// <summary>
        /// La descripcion del tipo de excepcion
        /// </summary>
        /// <returns></returns>
        public abstract string GetExceptionTypeDescription();




    }

    /// <summary>
    /// Tipos de errores HTTP mnejados
    /// </summary>
    public enum HttpErrorType
    {
        /// <summary>
        /// 
        /// </summary>
        Client = 400,
        /// <summary>
        /// 
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// 
        /// </summary>
        Forbidden = 403
    }
}
