using System;

namespace Ineval.DAL
{
    public interface IBaseEntity
    {
        DateTime FechaCreacion { get; set; }
        DateTime? FechaModificacion { get; set; }
        DateTime? FechaEliminacion { get; set; }
        EstadoEnum Estado { get; set; }
    }
}