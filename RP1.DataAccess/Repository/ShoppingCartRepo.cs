using RP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.DataAccess.Repository
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        private readonly AppDBContext _context;
        public ShoppingCartRepo(AppDBContext context)
        {
            _context = context;
        }

        public void Add(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            _context.SaveChanges();
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
            _context.SaveChanges();
        }

        public void Delete(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
            _context.SaveChanges();
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _context.ShoppingCarts.ToList();
        }

        public ShoppingCart? Get(int id)
        {
            return _context.ShoppingCarts.Find(id);
        }

        public ShoppingCart IncrementItem(string userid, int id)
        {
            var ShoppingCartItem = _context.ShoppingCarts.Where(p => p.ProductId == id && p.ApplicationUserId == userid).FirstOrDefault();
            return ShoppingCartItem ?? new ShoppingCart();
        }

        public int IncrementQty(ShoppingCart shoppingCart, int qty)
        {
            shoppingCart.Quantity += qty;
            _context.SaveChanges();
            return shoppingCart.Quantity;
        }
    }
}
