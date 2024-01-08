using Extension.Domain.Abstractions;
using Extension.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Extension.Domain.Repositories
{
    public class ProductRepository : BaseRepository<int, Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
        {

        }
    }
}
