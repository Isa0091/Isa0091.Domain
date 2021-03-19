using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Mvc.Filters.Filters
{
    /// <summary>
    /// Objeto para brindar m'as detalles del error en la llamada REST
    /// </summary>
    public class ApiExceptionResult
    {
        /// <summary>
        /// Codigo del tipo de error
        /// </summary>
        public int? ErrorTypeCode { get; set; }

        /// <summary>
        /// Nombre del tipo de error
        /// </summary>
        public string ErrorTypeName { get; set; }

        /// <summary>
        /// El mensaje d eerror
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// La claser en la que se detecto la excepcion
        /// </summary>
        public string RelatedObject { get; set; }
        /// <summary>
        /// El campo relacionado al error
        /// </summary>
        public string RelatedField { get; set; }
    }
}
