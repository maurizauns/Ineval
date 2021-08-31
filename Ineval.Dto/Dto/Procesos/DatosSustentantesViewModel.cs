using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Dto
{
    public class DatosSustentantesViewModel:BaseModel
    {
        public string usu_id { get; set; }
        public string tipo_identificacion { get; set; }
        public string identificacion { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string sexo { get; set; }
        public string grado { get; set; }
        public string dia_nacimiento { get; set; }
        public string mes_nacimiento { get; set; }
        public string anio_nacimiento { get; set; }
        public string pais_nacimiento { get; set; }
        public string provincia_nacimiento { get; set; }
        public string descapacidad { get; set; }
        public string tipo_discapacidad { get; set; }
        public string porcentaje_discapacidad { get; set; }
        public string correo_sustentante { get; set; }
        public string telefono_sustentante { get; set; }
        public string telefono_sustentante_secundario { get; set; }
        public string celular_sustentante { get; set; }
        public string jornada_sustentante { get; set; }
        public string amie { get; set; }
        public string nombre_institucion { get; set; }
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
        public string sostenimiento_institucion { get; set; }
        public string regimen_institucion { get; set; }
        public string ciclo { get; set; }
        public string población { get; set; }
    }
}
