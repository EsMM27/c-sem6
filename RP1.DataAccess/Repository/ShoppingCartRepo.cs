using Microsoft.EntityFrameworkCore;
using RP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.DataAccess.Repository
{
    public class ShoppingCartRepo : Repository<ShoppingCart>, IShoppingCartRepo
    {
        private readonly AppDBContext _context;
        public ShoppingCartRepo(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public void Add(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            _context.SaveChanges();
        }

        public int DecrementQty(ShoppingCart shoppingCart, int qty)
        {
            shoppingCart.Quantity -= qty;
            _context.SaveChanges();
            return shoppingCart.Quantity;
        }

        public IEnumerable<ShoppingCart> GetShoppingCartProduct(string userid)
        {
            var shoppingCart = _context.ShoppingCarts.Where(c => c.ApplicationUserId == userid).Include(p => p.Product).ThenInclude(u => u.Category);
            return shoppingCart;
        }

        public ShoppingCart IncrementItem(string applicationUserId, int productId)
        {
            return _context.ShoppingCarts.FirstOrDefault(c => c.ApplicationUserId == applicationUserId && c.ProductId == productId);
        }

        public int IncrementQty(ShoppingCart shoppingCart, int qty)
        {
            shoppingCart.Quantity += qty;
            _context.SaveChanges();
            return shoppingCart.Quantity;
        }

        public void RemoveAll(IEnumerable<ShoppingCart> items)
        {
            _context.ShoppingCarts.RemoveRange(items);
            _context.SaveChanges();
        }
    }
}
