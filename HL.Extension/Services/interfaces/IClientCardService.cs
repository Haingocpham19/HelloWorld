using Extension.Domain.Entities;

namespace PCS.Extension.Services.interfaces
{
    public interface IClientCardService
    {
        ServiceResult InsertClient(ClientCard entity);
    }
}
