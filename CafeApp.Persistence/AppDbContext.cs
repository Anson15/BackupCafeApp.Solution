using BackupCafeApp.DomainEntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Persistence
{
    public class AppDbContext : DbContext
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

