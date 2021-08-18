namespace Ineval.DAL
{
    public class Configuracion : BaseEntity
    {
        public string RutaAplicacion { get; set; }
        public int RegitrosPorPagina { get; set; }
        public string FormatoFecha { get; set; }
    }
}