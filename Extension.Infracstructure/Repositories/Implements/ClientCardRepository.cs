using Extension.Domain.Abstractions;
using Extension.Domain.Entities;

namespace Extension.Domain.Repositories
{
    public class ClientCardRepository : BaseRepository<Guid, ClientCard>, IClientCardRepository
    {
        public ClientCardRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
        {

        }
    }
}
