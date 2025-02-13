using RP1.DataAccess;
using RP1.DataAccess.Repository;

namespace RP1.Services
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDBContext _context;
        public ICategoryRepo CategoryRepo { get; private set; }
        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            CategoryRepo = new CategoryRepo(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
