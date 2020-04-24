using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupCafeApp.DomainEntityModel.Interface
{
    public interface IMenuRepository
    {
        IEnumerable<Menu> GetMenus();
        Menu GetMenu(int? id);
        void AddMenu(Menu menu);
        void DeleteMenu(Menu menu);
        void UpdateMenu(Menu menu);
        void Save();
    }
}
