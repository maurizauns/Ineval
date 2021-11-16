using System;

namespace Ineval.Dto
{
    public class DatosFiltrosViewModel: GeneralConfigurationViewModel
    {
        public Guid? AsignacionId { get; set; }
        public int? Filtro1 { get; set; }
        public string Filtro2 { get; set; }
        public string Filtro3 { get; set; }
        public string Filtro4 { get; set; }
        public string Filtro5 { get; set; }
    }
}
