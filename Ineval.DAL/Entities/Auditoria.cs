namespace Ineval.DAL
{
    public class Auditoria : BaseEntity
    {
        public Auditoria()
        {

        }

        public TipoErrorEnum TipoError { get; set; }
        public string Ruc { get; set; }
        public string Modulo { get; set; }
        public string Programa { get; set; }
        public string Accion { get; set; }
        public string Mensaje1 { get; set; }
        public string Mensaje2 { get; set; }
        public string Mensaje3 { get; set; }

    }
}