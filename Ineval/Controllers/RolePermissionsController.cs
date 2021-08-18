using Ineval.BO;
using Ineval.DAL;
using Ineval.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ineval.Controllers
{
    [Authorize]
    public class RolePermissionsController : Controller
    {
        public RoleMenuService roleMenuService = new RoleMenuService();
        public ActionResult Index(string id, string mensajes)
        {
            ViewBag.Title = "Permisos por Rol.";
            var model = SeleccionarRol(id);

            if (!string.IsNullOrEmpty(mensajes))
            {
                foreach (var mensaje in mensajes.Split(','))
                {
                    ModelState.AddModelError("", mensaje);
                }
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(RoleMenuViewModel model)
        {
            var result = roleMenuService.Save(model.ApplicationRoleId, model.PostedMenus.MenuIds);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", new { id = model.ApplicationRoleId });
            }
            else
            {
                return RedirectToAction("Index", new { id = model.ApplicationRoleId, mensajes = result.GetErrorsString() });
            }

        }

        private RoleMenuViewModel SeleccionarRol(string id)
        {

            var menus = roleMenuService.ObtenerMenus();

            var menusOrdenados = new List<Menu>();

            foreach (var menuPadre in menus.Where(m => m.ParentId == null).OrderBy(m => m.Orden))
            {
                menuPadre.Description = string.Format("<span class = 'menu-padre'>{0}</span>", menuPadre.Description);
                menusOrdenados.Add(menuPadre);
                foreach (var menuHijo in menus.Where(m => m.ParentId == menuPadre.Id).OrderBy(m => m.Orden))
                {
                    menuHijo.Description = string.Format("<span class = 'menu-hijo'>{0}</span>", menuHijo.Description);
                    menusOrdenados.Add(menuHijo);

                    foreach (var menuHijoSNivel in menus.Where(m => m.ParentId == menuHijo.Id).OrderBy(m => m.Orden))
                    {
                        menuHijoSNivel.Description = string.Format("<span class = 'menu-hijos'>{0}</span>", menuHijoSNivel.Description);
                        menusOrdenados.Add(menuHijoSNivel);

                        foreach (var menuHijoTNivel in menus.Where(m => m.ParentId == menuHijoSNivel.Id).OrderBy(m => m.Orden))
                        {
                            menuHijoTNivel.Description = string.Format("<span class = 'menu-hijot'>{0}</span>", menuHijoTNivel.Description);
                            menusOrdenados.Add(menuHijoTNivel);
                        }
                    }
                }
            }

            var model = new RoleMenuViewModel
            {
                AvailableMenus = menusOrdenados
            };
            var roles = roleMenuService.ObtenerRoles();
            id = id ?? roles[0].Id;
            ViewBag.ApplicationRoleId = new SelectList(roles, "Id", "Name", id);
            model.SelectedMenus = roleMenuService.ObtenerMenusPorRol(id);

            return model;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (roleMenuService != null)
                {
                    roleMenuService.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}