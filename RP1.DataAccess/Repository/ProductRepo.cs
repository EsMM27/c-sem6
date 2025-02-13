using RP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP1.DataAccess.Repository
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        private readonly AppDBContext _context;
        public ProductRepo(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            Console.WriteLine("Product being added: " + product.Name);
            Console.WriteLine("Image Path in DB: " + product.Image);
            _context.Products.Add(product);
        }


        public void Update(Product product)
        {
            var productFromDb = _context.Products.FirstOrDefault(p => p.ID == product.ID);

            if (productFromDb != null) //  Ensure product exists before updating
            {
                productFromDb.Name = product.Name;
                productFromDb.CategoryID = product.CategoryID;

                if (!string.IsNullOrEmpty(product.Image)) //  Update image only if a new one is provided
                {
                    productFromDb.Image = product.Image;
                }

                _context.Entry(productFromDb).State = Microsoft.EntityFrameworkCore.EntityState.Modified; //  Ensure EF tracks changes
            }
        }

    }
}
