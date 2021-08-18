using RP.DAL.Repository;
using Ineval.BO.Interface;
using System;

namespace Ineval.BO
{
    public class SwmServices : IUnitOfWorkService, IDisposable
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public SwmServices()
        {
            UnitOfWork = new UnitOfWork();
        }

        #region Services


        private AuditoriaService _auditoriaService;
        public AuditoriaService AuditoriaService
        {
            get
            {
                if (this._auditoriaService == null)
                {
                    return this._auditoriaService = new AuditoriaService(UnitOfWork);
                }
                return _auditoriaService;
            }
        }


        private UsuarioService _usuarioService;
        public UsuarioService UsuarioService
        {
            get
            {
                if (this._usuarioService == null)
                {
                    return this._usuarioService = new UsuarioService(UnitOfWork);
                }
                return _usuarioService;
            }
        }


        #endregion

        public void Dispose()
        {
            if (_auditoriaService != null)
            {
                _auditoriaService.Dispose();
            }

            if (_usuarioService != null)
            {
                _usuarioService.Dispose();
            }

        }
    }
}
