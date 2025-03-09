using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int QtyOrdered { get; set; }
        //f key
        public int OrderId { get; set; }
        //Reference Nav property
        public Order Order { get; set; }
    }
}
