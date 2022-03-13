using AppFerreteria.Data;
using AppFerreteria.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AppFerreteria
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Data.AppFerreteriaContext, Migrations.Configuration>());

            ApplicationDbContext db = new ApplicationDbContext();
            AppFerreteriaContext dbe = new AppFerreteriaContext();

            CrearUsuario(db);
        }

        private void CrearUsuario(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindByEmail("julian_guastoni@hotmail.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "julian_guastoni@hotmail.com",
                    Email = "julian_guastoni@hotmail.com",
                };
                userManager.Create(user, "123456");
            }
        }
    }
}
