using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.DataAccess.Repository
{
    public class OrderItemRepo : IOrderItemRepo
    {
        private readonly AppDBContext _context;
        public OrderItemRepo(AppDBContext context)
        {
            _context = context;
        }
    }
}
