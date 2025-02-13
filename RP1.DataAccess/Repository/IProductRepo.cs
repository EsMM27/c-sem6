using RP1.Models;

namespace RP1.DataAccess.Repository
{
    public interface IProductRepo : IRepository<Product>
    {
        public void Update(Product product);
    }
}
