using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ineval.Common
{
    public static class Context
    {

        public static int PageSize = 10;
        public static string FormatoFecha = "dd/MM/yyyy";
        public static string FormatoFechaI = "MM/dd/yyyy";
        public static string FormatoHora = "HH:mm:ss";
        public static Guid UserId = Guid.Empty;

        //public static Guid UserId()
        //{
        //    using (var usuario = new UsuarioService())
        //    {
        //        var usuarioData = usuario.ObtenerPorApplicationUserId(UserOnlineId);
        //        return usuarioData.Id;
        //    }
        //}

        public static int NumeroDecimales = 2;
        public static string FormatoFechaHora
        {
            get { return string.Format("{0} {1}", FormatoFecha, FormatoHora); }
        }
        public static string FormatoFechaHoraI
        {
            get { return string.Format("{0} {1}", FormatoFechaI, FormatoHora); }
        }
        public static NumberFormatInfo numberFormatInfo = new CultureInfo("en-US", false).NumberFormat;

        public static string RutaAplicacion;

    }
}
