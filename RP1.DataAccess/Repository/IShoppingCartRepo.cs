using RP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.DataAccess.Repository
{
    public interface IShoppingCartRepo : IRepository<ShoppingCart>
    {
        void Add(ShoppingCart shoppingCart);
        ShoppingCart IncrementItem(string userid, int id);
        int IncrementQty(ShoppingCart shoppingCart, int qty);
    }
}
