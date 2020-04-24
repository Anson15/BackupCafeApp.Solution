using BackupCafeApp.DomainEntityModel;
using BackupCafeApp.DomainEntityModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Persistence.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        public AppDbContext db = new AppDbContext();
        public void AddMenu(Menu menu)
        {
            db.Menus.Add(menu);
            Save();
        }

        public void DeleteMenu(Menu menu)
        {
            db.Menus.Remove(menu);
            Save();
        }

        public Menu GetMenu(int? id)
        {
            Menu menu = db.Menus.Find(id);
            return menu;
        }

        public IEnumerable<Menu> GetMenus()
        {
            return db.Menus.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateMenu(Menu menu)
        {
            db.Entry(menu).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
