using System;

namespace Ineval.DAL
{
    public class DatosFiltros : GeneralConfigurationBase
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
        public int? Filtro1 { get; set; }
        public int? Filtro2 { get; set; }
        public int? Filtro3 { get; set; }
        public int? Filtro4 { get; set; }
        public int? Filtro5 { get; set; }
    }
}
