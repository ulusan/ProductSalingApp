using ProductSaling.Core.IRepository;
using ProductSaling.Core.Repository;
using ProductSaling.Core.UnitOfWork.Abstract;
using ProductSaling.Data;
using ProductSaling.Data.Entities;

namespace ProductSaling.Core.UnitOfWork.Concrete
{
    /// <summary>
    /// UnitOfWork Pattern allows to do all the operations 
    /// to be done with the database through a single channel.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Product> _products;
        private IGenericRepository<Category> _categories;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<Customer> _customers;

        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context);

        public IGenericRepository<Customer> Customers => _customers ??= new GenericRepository<Customer>(_context);

        public IGenericRepository<Order> Orders => _orders ??= new GenericRepository<Order>(_context);

        public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
