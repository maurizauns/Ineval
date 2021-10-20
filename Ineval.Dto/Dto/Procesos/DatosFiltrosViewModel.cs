using System;

namespace Ineval.Dto
{
    public class DatosFiltrosViewModel: GeneralConfigurationViewModel
    {
        public Guid? AsignacionId { get; set; }
        public int? Filtro1 { get; set; }
        public int? Filtro2 { get; set; }
        public int? Filtro3 { get; set; }
        public int? Filtro4 { get; set; }
        public int? Filtro5 { get; set; }
    }
}
