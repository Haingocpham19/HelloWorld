using Extension.Domain.Entities;
using Extension.Domain.Repositories;
using PCS.Extension.Services.interfaces;

namespace PCS.Extension.Services.implement
{
    public class CurrencyService : BaseService<Currency>, ICurrencyService
    {
        public CurrencyService(IBaseRepository<Currency> baseRepository) : base(baseRepository)
        {

        }
    }
}
