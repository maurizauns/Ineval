using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("DatosLaboratorio")]
    public class DatosLaboratorio : BaseEntity
    {
        public Guid? AsignacionId { get; set; }
        public virtual Asignacion Asignacion { get; set; }
        public string sede { get; set; }
        public string sede_institucion { get; set; }
        public string id_provincia { get; set; }
        public string provincia { get; set; }
        public string canton_id { get; set; }
        public string canton { get; set; }
        public string id_parroquia { get; set; }
        public string parroquia { get; set; }
        public string id_circuito { get; set; }
        public string circuito { get; set; }
        public string id_distrito { get; set; }
        public string distrito { get; set; }
        public string id_zona { get; set; }
        public string direccion { get; set; }
        public string referencia { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string celular { get; set; }
        public string sostenimiento { get; set; }
        public string regimen { get; set; }
        public string jornada { get; set; }
        public string codigo_laboratorio { get; set; }
        public string computadora_laboratorio { get; set; }
        public string coordenada_Lat { get; set; }
        public string coordenada_Lng { get; set; }
    }
}
