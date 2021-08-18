using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RP.DAL.Repository;
using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ineval.BO
{
    public class RoleMenuService : IService
    {
        SwmContext Context = new SwmContext();
        public RoleMenuService()
        {

        }

        public List<ApplicationRole> ObtenerRoles()
        {
            var roleStore = new RoleStore<ApplicationRole>(Context);
            var roleManager = new RoleManager<ApplicationRole>(roleStore);

            try
            {
                return roleManager.Roles.ToList();
            }
            catch (Exception)
            {
                return new List<ApplicationRole>();
            }
            finally
            {
                roleStore.Dispose();
                roleManager.Dispose();
            }

        }

        public List<Menu> ObtenerMenus()
        {
            using (var menuService = new MenuService())
            {
                return menuService.GetAll().ToList();
            }
        }

        public SaveResult Save(string roleId, IEnumerable<Guid> menuIds)
        {
            var roleStore = new RoleStore<ApplicationRole>(Context);
            var roleManager = new RoleManager<ApplicationRole>(roleStore);

            try
            {
                var menus = Context.Menus.Where(x => menuIds.Any(s => x.Id.Equals(s))).ToList();

                var role = roleManager.FindById(roleId);

                var oldMenus = role.Menus.Where(m => menus.All(m2 => m2.Id != m.Id)).ToList();
                var newMenus = menus.Where(m => role.Menus.All(m2 => m2.Id != m.Id)).ToList();

                foreach (var newMenu in newMenus)
                {
                    role.Menus.Add(newMenu);

                }
                foreach (var oldMenu in oldMenus)
                {
                    role.Menus.Remove(oldMenu);
                }

                Context.SaveChanges();

                return SaveResult.Success();
            }
            catch (Exception ex)
            {
                return ex.SaveResult();
            }
            finally
            {
                roleStore.Dispose();
                roleManager.Dispose();
            }
        }

        public List<Menu> ObtenerMenusPorRol(string roleId)
        {

            if (string.IsNullOrEmpty(roleId))
            {
                return new List<Menu>();
            }

            var roleStore = new RoleStore<ApplicationRole>(Context);
            var roleManager = new RoleManager<ApplicationRole>(roleStore);

            try
            {
                var role = roleManager.Roles.FirstOrDefault(r => r.Id == roleId);
                return role != null ? role.Menus.ToList() : new List<Menu>();
            }
            catch (Exception)
            {
                return new List<Menu>();
            }
            finally
            {
                roleStore.Dispose();
                roleManager.Dispose();
            }
        }

        public IEnumerable<string> ObtenerRolPorUserId(string userId)
        {
            var userStore = new UserStore<ApplicationUser>(Context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            try
            {
                return userManager.GetRoles(userId);
            }
            finally
            {
                userStore.Dispose();
                userManager.Dispose();
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
