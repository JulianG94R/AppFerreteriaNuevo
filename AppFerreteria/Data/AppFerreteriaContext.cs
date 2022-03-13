using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppFerreteria.Data
{
    public class AppFerreteriaContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AppFerreteriaContext() : base("name=AppFerreteriaContext")
        {
        }

        public System.Data.Entity.DbSet<AppFerreteria.Models.Clientes> Clientes { get; set; }

        public System.Data.Entity.DbSet<AppFerreteria.Models.Motosierras> Motosierras { get; set; }

        public System.Data.Entity.DbSet<AppFerreteria.Models.Alquiler> Alquilers { get; set; }

        public System.Data.Entity.DbSet<AppFerreteria.Models.Devolucion> Devolucions { get; set; }
    }
}
