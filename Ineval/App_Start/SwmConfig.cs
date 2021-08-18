using Ineval.BO;
using Ineval.Common;


namespace Ineval.App_Start
{
    public static class InevalConfig
    {
        public static void Init()
        {
            var config = SettingService.ObtenerConfiguracion();

            Context.RutaAplicacion = config.RutaAplicacion;
            //ConfiguracionManager.RutaAplicacion = Context.RutaAplicacion; //Establece la Ruta a las Dlls del Servicio Firmador

            Context.PageSize = config.RegitrosPorPagina;
            Context.FormatoFecha = config.FormatoFecha;

            // ConfiguracionManager.ObtenerConfiguracion(); //Crea la configuración general de la aplicación
        }
    }
}