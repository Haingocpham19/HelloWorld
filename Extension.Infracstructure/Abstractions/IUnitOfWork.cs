using System.Threading.Tasks;

namespace Extension.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
