using BackupCafeApp.DomainEntityModel;
using BackupCafeApp.DomainEntityModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Persistence.Repositories
{
    public class CartRepository : ICartRepostory
    {
        public AppDbContext db = new AppDbContext();
        public void AddCart(Cart cart)
        {
            db.Carts.Add(cart);
            Save();
        }

        public void DeleteCart(Cart cart)
        {
            db.Carts.Remove(cart);
            Save();
        }

        public Cart GetCart(int? id)
        {
            Cart cart = db.Carts.Find(id);
            return cart;
        }

        public IEnumerable<Cart> GetCarts()
        {
            return db.Carts.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateCart(Cart cart)
        {
            db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
