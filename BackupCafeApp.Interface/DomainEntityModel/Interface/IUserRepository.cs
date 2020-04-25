using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupCafeApp.Interface.DomainEntityModel.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUser(int? id);
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        void GetUserbyRoles(User user);
        void Save();
    }
}
