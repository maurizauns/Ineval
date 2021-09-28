using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ineval.DAL
{
    public class Usuario : BaseEntity
    {
        public Usuario()
        {
            //Documentos = new HashSet<Documento>();
        }

        public string Identificacion { get; set; }
        public string TipoIdentificacion { get; set; }
        public string NombresCompletos { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Email { get; set; }
        public string Permissions { get; set; }

        [NotMapped]
        public string ApplicationRoleName { get; set; }
        public string APIKEY { get; set; }
    }
}