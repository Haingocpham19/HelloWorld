using Extension.Domain.Entities;

namespace PCS.Extension.Services.interfaces
{
    public interface IProductService
    {
        ServiceResult GetProductsByIdClient(Guid clientId);
        ServiceResult InsertProducts(Products product);
    }
}
