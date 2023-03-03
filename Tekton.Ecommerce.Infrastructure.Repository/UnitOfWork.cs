using Tekton.Ecommerce.Infrastructure.Interface;

namespace Tekton.Ecommerce.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductsRepository Product { get; }

        public UnitOfWork(IProductsRepository product)
        {
            Product = product;
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
