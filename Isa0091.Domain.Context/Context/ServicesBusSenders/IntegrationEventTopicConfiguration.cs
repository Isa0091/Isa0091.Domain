using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Context.ServicesBusSenders
{
    /// <summary>
    /// Configuracion de los topics creados para el despacho de IIntegrationEvents
    /// </summary>
    public class IntegrationEventTopicConfiguration
    {
        /// <summary>
        /// El tiempo en segundos que el mensaje puede eatr en el topica antes de vencerse
        /// </summary>
        public int TimeToLiveSeconds{ get; set; }

        /// <summary>
        /// Si el topi se borra cuando esta sin actividad pro mucho tiempo
        /// </summary>
        public int? AutoDeleteSeconds { get; set; }

        /// <summary>
        /// Si se especifica si activa la deteccion de duplivcidad con el tiempo establecido
        /// </summary>
        public int? DuplicateDetectionSeconds { get; set; }

        /// <summary>
        /// El tamaño maximo del topic en bytez, si no se especiica se usa el por defceto de azure
        /// </summary>
        public long? MaxSizeInMegabytes { get; set; }


        /// <summary>
        /// Si se ahbilita las partaciones
        /// </summary>
        public bool EnablePatitioning { get; set; }

        /// <summary>
        /// String de conexion del service bus, debe permitir crear topics
        /// </summary>
        public string ConnectionString { get; set; }

    }
}
