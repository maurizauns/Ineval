using System;

namespace Ineval.DAL
{
    public class DatosFiltros : GeneralConfigurationBase
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
        public int? Filtro1 { get; set; }
        public string Filtro2 { get; set; }
        public string Filtro3 { get; set; }
        public string Filtro4 { get; set; }
        public string Filtro5 { get; set; }
    }
}
