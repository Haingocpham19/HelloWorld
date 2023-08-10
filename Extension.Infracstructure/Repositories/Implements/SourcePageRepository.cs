using Extension.Domain.Abstractions;
using Extension.Domain.Entities;

namespace Extension.Domain.Repositories
{
    public class SourcePageRepository : BaseRepository<SourcePage>, ISourcePageRepository
    {
        public SourcePageRepository(IDbFactory dbFactory, IUnitOfWork unitOfWork) : base(dbFactory, unitOfWork)
        {

        }
    }
}
