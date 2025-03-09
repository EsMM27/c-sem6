using RP1.DataAccess;
using RP1.DataAccess.Repository;

namespace RP1.Services
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDBContext _context;
        public ICategoryRepo CategoryRepo { get; private set; }
        public IProductRepo ProductRepo { get; private set; }
        public IOrderRepo OrderRepo { get; private set; }
        public IOrderItemRepo OrderItemRepo { get; private set; }
        public IApplicationUserRepo ApplicationUserRepo { get; private set; }
        public IShoppingCartRepo ShoppingCartRepo { get; private set; }

        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            CategoryRepo = new CategoryRepo(_context);
            ProductRepo = new ProductRepo(_context);
            OrderRepo = new OrderRepo(_context);
            OrderItemRepo = new OrderItemRepo(_context);
            ApplicationUserRepo = new ApplicationUserRepo(_context);
            ShoppingCartRepo = new ShoppingCartRepo(_context);
        }
        public async Task SaveAsync()
        {
            Console.WriteLine("Saving changes to the database...");
            await _context.SaveChangesAsync();
            Console.WriteLine("Database changes saved.");
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
