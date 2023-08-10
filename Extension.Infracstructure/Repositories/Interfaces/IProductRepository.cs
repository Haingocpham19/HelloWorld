using Extension.Domain.Entities;

namespace Extension.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetProductsByIdClientAsync(object clientId);
    }
}
