using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Diagnostics;

namespace Ineval.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<SwmContext>
    {
         private readonly bool _exists;
        public Configuration()
        {
            AutomaticMigrationsEnabled =
                Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["MigrateDatabaseToLatestVersion"]);

            AutomaticMigrationDataLossAllowed = true;

            using (DbContext context = new SwmContext())
            {
                _exists = context.Database.Exists();
            }
        }

        protected override void Seed(SwmContext context)
        {
            if (_exists)
            {
                return;
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            const string email = "Administrador";
            var user = new ApplicationUser { UserName = email, Email = email };
            var result = userManager.Create(user, "Admin123.");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Debug.Print(error);
                }
            }

            var roleStore = new RoleStore<ApplicationRole>(context);
            var roleManager = new RoleManager<ApplicationRole>(roleStore);
            var rolAdministrador = roleManager.FindByName("Administrador");
            var rolUsuario = roleManager.FindByName("Usuario");
            var rolCliente = roleManager.FindByName("Cliente");
            var rolEmpleado = roleManager.FindByName("Empleado");
            var rolPersona = roleManager.FindByName("Persona");

            if (rolAdministrador == null)
            {
                rolAdministrador = new ApplicationRole { Name = "Administrador" };
                roleManager.Create(rolAdministrador);

                rolUsuario = new ApplicationRole { Name = "Usuario" };
                roleManager.Create(rolUsuario);

                rolCliente = new ApplicationRole { Name = "Cliente" };
                roleManager.Create(rolCliente);

                rolEmpleado = new ApplicationRole { Name = "Empleado" };
                roleManager.Create(rolEmpleado);

                rolPersona = new ApplicationRole { Name = "Persona" };
                roleManager.Create(rolPersona);
            }

            var manager = userManager.FindByName(user.UserName);
            if (manager != null)
            {
                userManager.AddToRole(manager.Id, "Administrador");
            }

            var menuConfiguracion = new Menu
            {
                Nombre = "Administracion",
                Description = "Administración",
                Orden = 1,
                Url = ""
            };
        }
    }
}
