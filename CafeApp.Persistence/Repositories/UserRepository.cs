using BackupCafeApp.DomainEntityModel;
using BackupCafeApp.DomainEntityModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BackupCafeApp.DomainEntityModel.User;

namespace CafeApp.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public AppDbContext db = new AppDbContext();
        public void AddUser(User user)
        {
            db.Users.Add(user);
            Save();
        }

        public void DeleteUser(User user)
        {
            db.Users.Remove(user);
            Save();

        }

        public User GetUser(int? id)
        {
            User user = db.Users.Find(id);
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
        public User GetUserbyUsername(User user)
        {
            var users=db.Users.Where(u => u.Username == user.Username).SingleOrDefault();
            return users;

        }
    }
}
