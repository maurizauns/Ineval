using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RP.DAL.Repository;
using Ineval.DAL;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Ineval.BO
{
    public class UsuarioService : EntityService<Usuario>, IService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleStore<ApplicationRole> _roleStore;
        private RoleManager<ApplicationRole> _roleManager;

        public UsuarioService()
        {

        }
        public UsuarioService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public override async Task<SaveResult> SaveAsync(Usuario entity)
        {
            var roleName = string.IsNullOrEmpty(entity.ApplicationRoleName) ? "Cliente" : entity.ApplicationRoleName;
            return await SaveAsync(entity, roleName);
        }

        public async Task<SaveResult> SaveAsync(Usuario entity, string roleName)
        {
            try
            {
                using (var tran = UnitOfWork.StartTransaction())
                {
                    try
                    {
                        _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UnitOfWork.Db));
                        _roleStore = new RoleStore<ApplicationRole>(UnitOfWork.Db);
                        _roleManager = new RoleManager<ApplicationRole>(_roleStore);

                        if (entity.IsNew())
                        {
                            var userAplication = new ApplicationUser { UserName = entity.Identificacion, Email = entity.Email };
                            var result = await _userManager.CreateAsync(userAplication, entity.Identificacion);

                            if (!result.Succeeded)
                            {
                                throw new Exception("Error al agregar usuario " + string.Join(",", result.Errors.ToArray()));
                            }

                            entity.ApplicationUserId = userAplication.Id;
                            entity.FechaCreacion = DateTime.Now;
                            entity.Estado = EstadoEnum.Activo;

                            await _userManager.AddToRoleAsync(userAplication.Id, roleName);

                            await CreateAsync(entity);
                        }
                        else
                        {
                            var userAplication = await _userManager.FindByIdAsync(entity.ApplicationUserId);
                            var roles = await _userManager.GetRolesAsync(userAplication.Id);

                            foreach (var role in roles)
                            {
                                if (role != roleName)
                                {
                                    await _userManager.RemoveFromRoleAsync(userAplication.Id, role);
                                }
                            }

                            var isInRole = roles.Any(r => r.Trim().ToUpper().Equals(roleName.Trim().ToUpper()));
                            if (!isInRole)
                            {
                                await _userManager.AddToRoleAsync(userAplication.Id, roleName);
                            }

                            userAplication.Email = entity.Email;
                            userAplication.FechaModificacion = DateTime.Now;
                            UnitOfWork.Db.Entry(userAplication).State = EntityState.Modified;

                            await UpdateAsync(entity);
                        }
                        await UnitOfWork.CommitAsync();
                        return SaveResult.Success(entity);
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
            finally
            {
                _userManager.Dispose();
                _roleStore.Dispose();
                _roleManager.Dispose();
            }
        }

        public override async Task<SaveResult> DeleteAsync(Guid id)
        {
            var usuario = await GetByIdAsync(id);
            return await DeleteAsync(usuario);
        }

        public override async Task<SaveResult> DeleteAsync(Usuario entity)
        {
            try
            {
                using (var tran = UnitOfWork.StartTransaction())
                {
                    await base.DeleteAsync(entity);

                    _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UnitOfWork.Db));
                    var userAplication = await _userManager.FindByIdAsync(entity.ApplicationUserId);

                    await _userManager.DeleteAsync(userAplication);

                    await UnitOfWork.CommitAsync();
                }

                return SaveResult.Success(entity);
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
        }

        public static string GetTipoIdentificacion(string codigo)
        {
            return codigo == "C" ? "Cédula" : codigo == "R" ? "Ruc" : codigo == "P" ? "Pasaporte" : "No asignado";
        }

        public async Task<SaveResult> CambiarClave(Guid userId, string claveAnterior, string nuevaClave)
        {
            try
            {
                _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UnitOfWork.Db));

                var usuario = GetById(userId);

                var applicationUser = await _userManager.FindByIdAsync(usuario.ApplicationUserId);

                IdentityResult result;

                if (applicationUser.PasswordHash != null)
                {
                    result = await _userManager.ChangePasswordAsync(usuario.ApplicationUserId, claveAnterior, nuevaClave);
                }
                else
                {
                    result = await _userManager.AddPasswordAsync(usuario.ApplicationUserId, nuevaClave);
                }

                return result.Succeeded ? SaveResult.Success() : SaveResult.Failed(result.Errors);
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
            finally
            {
                _userManager.Dispose();
            }
        }

        /// <summary>
        /// Verifica si existe usuario en la Base de Datos
        /// </summary>
        /// <param name="identificacion">Identificacion (Ruc/Cedula/Pasaporte) Usuario</param>
        /// <returns></returns>
        public bool Existe(string identificacion)
        {
            return Contains(u => u.Identificacion == identificacion);
        }

        public Usuario ObtenerPorIdentificacion(string identificacion)
        {
            return FirstOrDefault(u => u.Identificacion == identificacion);
        }

        public Usuario ObtenerPorApplicationUserId(string id)
        {
            var result = FirstOrDefault(up => up.ApplicationUserId == id);
            return result;
        }

        public bool IsAuthorizedPage(string userId, string modulo, string accion)
        {
            using (var contexto = new SwmContext())
            {
                var consulta = (
                    from m in contexto.Menus
                    where m.Roles.Any(r => contexto.Users.FirstOrDefault(u => u.Id == userId).Roles.Any(ur => ur.RoleId == r.Id))
                          && m.Nombre.ToUpper().Equals(modulo)
                    select m);

                return consulta.Any();
            }
        }

        public override void Dispose()
        {
            if (_userManager != null)
            {
                _userManager.Dispose();
            }

            base.Dispose();
        }
    }
}
