using BackupCafeApp.Interface.DomainEntityModel;
using BackupCafeApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupCafeApp.Interface.Persistence
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
             : base("name=AppDbContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
    }
}
