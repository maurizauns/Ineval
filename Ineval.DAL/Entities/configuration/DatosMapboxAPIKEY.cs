using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    [Table("DatosMapboxAPIKEY")]
    public class DatosMapboxAPIKEY : GeneralConfigurationBase    {
        
        public string APIKEY { get; set; }
        public int NumeroMaximoConsulta { get; set; }
        public int NumeroMininoConsulta { get; set; }
        public int NumeroUsadasConsultas { get; set; }
    }
}
