using Extension.Domain.Abstractions;
using Extension.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Extension.Domain.Repositories
{ 
    public class ProductRepository : BaseRepository<Products>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
        {

        }

        public IEnumerable<Products> GetProductsByIdClient(object clientId)
        {
            var listProducts = DbContext.Products.Where(x => x.ClientCardId == clientId);
            if (listProducts.Count() > 0)
                return listProducts??null;
            return null;
        }
    }
}
