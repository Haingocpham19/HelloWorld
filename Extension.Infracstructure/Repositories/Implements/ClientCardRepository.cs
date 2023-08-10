using Extension.Domain.Abstractions;
using Extension.Domain.Entities;

namespace Extension.Domain.Repositories
{
    public class ClientCardRepository : BaseRepository<ClientCard>, IClientCardRepository
    {
        public ClientCardRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
        {

        }
    }
}
