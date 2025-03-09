using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.DataAccess.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDBContext _context;

        public OrderRepo(AppDBContext context)
        {
            _context = context;
        }
    }
}
