using RP.DAL.Repository;
using Ineval.DAL;
using System;

namespace Ineval.BO
{
    public class SettingService : EntityService<Configuracion>, IService
    {
        public SettingService()
        {

        }

        public SettingService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public static Configuracion ObtenerConfiguracion()
        {
            var configService = new SettingService();
            var config = configService.FirstOrDefault();
            if (config == null)
            {
                throw new Exception("Error al obtener configuracion");
            }
            configService.Dispose();

            return config;
        }
    }
}
