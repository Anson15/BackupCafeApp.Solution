using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupCafeApp.DomainEntityModel.Interface
{
    public interface ICartRepostory
    {
        IEnumerable<Cart> GetCarts();
        Cart GetCart(int? id);
        void AddCart(Cart cart);
        void DeleteCart(Cart cart);
        void UpdateCart(Cart cart);
        void Save();
    }
}
