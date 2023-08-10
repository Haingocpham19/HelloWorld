using Extension.Domain.Abstractions;
using Extension.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Extension.Domain.Repositories
{
    public class ProductRepository : BaseRepository<Products>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
        {

        }

        public async Task<IEnumerable<Products>> GetProductsByIdClientAsync(object clientId)
        {
            var listProducts = await base._dbSet.Where(x => x.ClientCardId == (string)clientId).ToListAsync();
            return listProducts;
        }
    }
}
