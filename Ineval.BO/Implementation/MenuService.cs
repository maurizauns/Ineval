using RP.DAL.Repository;
using Ineval.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ineval.BO
{
    public class MenuService : EntityService<Menu>, IService
    {
        public MenuService()
        {

        }

        public MenuService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public static IEnumerable<Menu> GetMenuByIdUser(string userId)
        {
            try
            {
                using (var contexto = new SwmContext())
                {
                    var consulta = (
                                    from m in contexto.Menus
                                    where m.Roles.Any(
                                           r => contexto.Users.FirstOrDefault(u => u.Id == userId)
                                               .Roles.Any(ur => ur.RoleId == r.Id))
                                    select m
                                ).ToList();

                    List<Guid> existeMenu = new List<Guid>();
                    var menuLista = consulta.Where(m => m.ParentId == null).OrderBy(m => m.Orden).ToList();
                    // var menuLista = consulta.OrderBy(m => m.Orden).ToList();

                    //foreach (var item in menuLista)
                    //{
                    //    existeMenu.Add(item.Id);
                    //    int existe = consulta.Where(x => x.ParentId == item.Id).Count();
                    //    if (existeMenu.Contains(item.Id) && existe > 0)
                    //    {

                    //        if (existe > 0)
                    //        {
                    //            item.MenuItems = consulta.Where(m => m.ParentId == item.Id).OrderBy(m => m.Orden).ToList();
                    //            foreach (var item1 in item.MenuItems)
                    //            {
                    //                existeMenu.Add(item1.Id);
                    //            }
                    //        }
                    //    }
                    //}

                    foreach (var menu in menuLista)
                    {
                        var listaPrimerNivel = consulta.Where(m => m.ParentId == menu.Id).OrderBy(m => m.Orden).ToList();
                        foreach (var item in listaPrimerNivel)
                        {
                            var listaSegundoNivel = consulta.Where(m => m.ParentId == item.Id).OrderBy(m => m.Orden).ToList();
                            foreach (var item1 in listaSegundoNivel)
                            {
                                var listaTerverNivel = consulta.Where(m => m.ParentId == item1.Id).OrderBy(m => m.Orden).ToList();
                                foreach (var item2 in listaTerverNivel)
                                {

                                }
                                item1.MenuItems = listaTerverNivel;
                            }
                            item.MenuItems = listaSegundoNivel;
                        }
                        menu.MenuItems = listaPrimerNivel;
                    }

                    //foreach (var menu in menuLista)
                    //{
                    //    menu.MenuItems = consulta.Where(m => m.ParentId == menu.Id).OrderBy(m => m.Orden).ToList();
                    //}
                    return menuLista;
                }
            }
            catch (Exception)
            {
                return new List<Menu>();
            }

        }


    }
}
