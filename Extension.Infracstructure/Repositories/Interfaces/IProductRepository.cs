using Extension.Domain.Entities;
using System.Collections.Generic;

namespace Extension.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Products> GetProductsByIdClient(object clientId);
    }
}
