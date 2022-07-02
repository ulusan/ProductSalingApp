using ProductSaling.Core.IRepository;
using ProductSaling.Data.Entities;

namespace ProductSaling.Core.UnitOfWork.Abstract
{
    /// <summary>
    /// UnitOfWork Pattern allows to do all the operations 
    /// to be done with the database through a single channel.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<Category> Categories { get; }

        Task Save();
    }
}
