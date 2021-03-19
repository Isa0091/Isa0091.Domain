using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Core.ValueObjects
{
    /// <summary>
    /// Indica que es un valu object con cosdgiod e validacuon
    /// </summary>
    public interface IValidatedValueObject
    {
        /// <summary>
        /// Ejecuta el codigo de la validacion y genera errores en vcaso hayan validaciones que no pasen.
        /// </summary>
        void IsValid();
    }
}
