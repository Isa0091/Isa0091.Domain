using System;
using System.Collections.Generic;
using System.Text;

namespace Isa0091.Domain.Core.Model
{
    public abstract class RootEntity : Entity
    {
        readonly List<Events.IEvent> _eventos;

        /// <summary>
        /// 
        /// </summary>
        public RootEntity()
        {
            _eventos = new List<Events.IEvent>();
        }

        /// <summary>
        /// Permite agregar domain events que deben dispararse
        /// </summary>
        /// <param name="evento"></param>
        public void AgregarEvento(Events.IEvent evento)
        {
            _eventos.Add(evento);
        }

        /// <summary>
        /// Permite quitar un domain event
        /// </summary>
        /// <param name="evento"></param>
        public void QuitarEvento(Events.IEvent evento)
        {
            _eventos?.Remove(evento);
        }

        /// <summary>
        /// Saca la lsita de eventos pendientes de despachar
        /// </summary>
        /// <returns></returns>
        public List<Events.IEvent> GetEventos()
        {
            return Eventos;
        }

        /// <summary>
        /// Saca todos los eventos a disparar
        /// </summary>
        private List<Events.IEvent> Eventos => _eventos;
    }
}
