using RP1.DataAccess.Repository;

namespace RP1.Services
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepo CategoryRepo { get; }

        Task SaveAsync();

        IProductRepo ProductRepo { get; }
    }
}
