using Extension.Domain.Abstractions;
using Extension.Domain.Entities;

namespace Extension.Domain.Repositories
{
    public class CurrencyRepository : BaseRepository<int, Currency>, ICurrencyRepository
    {
        public CurrencyRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
        {

        }
    }
}
