namespace Tekton.Ecommerce.Infrastructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IProductsRepository Product { get; }
    }
}
