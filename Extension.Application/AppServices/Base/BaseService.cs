using AutoMapper;
using AutoMapper.Internal.Mappers;
using Extension.Application.AppFactory;
using Extension.Application.Dto.Base;
using Extension.Domain.Common;
using Extension.Domain.Repositories;
using static Extension.Domain.Enum.ExtensionEnums;

namespace Extension.Services
{
    public class CrudBaseService<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput> : IBaseService<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput>
        where TEntity : class
        where TEntityDto : class
        where TPagedInput : PagedFullInputDto
        where TPagedOutput : class//, IEntity<TPrimaryKey>
    {
        protected readonly IAppFactory Factory;
        protected readonly IBaseRepository<TPrimaryKey, TEntity> Repository;
        private readonly IMapper ObjectMapper;

        protected CrudBaseService(IAppFactory factory)
        {
            Factory = factory;
            ObjectMapper = Factory.ObjectMapper;
            Repository = Factory.Repository<TPrimaryKey, TEntity>();
        }

        public async Task<CommonResultDto<TEntityDto>> InsertAsync(TEntityDto dto)
        {
            try
            {
                var entity = ObjectMapper.Map<TEntity>(dto);
                var result = Repository.InsertAsync(entity);

                return new CommonResultDto<TEntityDto>
                {
                    IsSuccessful = true,
                    ErrorMessage = "",
                    StatusCode = System.Net.HttpStatusCode.OK,
                    DataResult = Factory.ObjectMapper.Map<TEntityDto>(result)
                };
            }
            catch (Exception ex)
            {
                return new CommonResultDto<TEntityDto>
                {
                    IsSuccessful = false,
                    ErrorMessage = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }

        public async Task<CommonResultDto<TEntityDto>> UpdateAsync(TEntityDto dto)
        {
            try
            {
                var entity = Factory.ObjectMapper.Map<TEntity>(dto);

                var result = await Repository.UpdateAsync(entity);

                return new CommonResultDto<TEntityDto>
                {
                    IsSuccessful = true,
                    ErrorMessage = "",
                    StatusCode = System.Net.HttpStatusCode.OK,
                    DataResult = Factory.ObjectMapper.Map<TEntityDto>(result)
                };

            }
            catch (Exception ex)
            {
                return new CommonResultDto<TEntityDto>
                {
                    IsSuccessful = false,
                    ErrorMessage = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
        }
        public async Task<CommonResultDto<TPrimaryKey>> DeleteAsync(TPrimaryKey Id)
        {
            try
            {
                var entity = Repository.GetByIdAsync(Id);
                if (entity != null)
                {
                    var result = await Repository.DeleteAsync(Id);
                    if (result != null)
                    {
                        return new CommonResultDto<TPrimaryKey>
                        {
                            IsSuccessful = true,
                            ErrorMessage = "",
                            StatusCode = System.Net.HttpStatusCode.OK,
                            DataResult = Id
                        };
                    }
                    else
                    {
                        return new CommonResultDto<TPrimaryKey>
                        {
                            IsSuccessful = true,
                            ErrorMessage = "",
                            StatusCode = System.Net.HttpStatusCode.OK,
                            DataResult = Id,
                            MessageCode = MessageCode.IsValid
                        };
                    }
                }
                else
                {
                    return new CommonResultDto<TPrimaryKey>
                    {
                        IsSuccessful = true,
                        ErrorMessage = "",
                        StatusCode = System.Net.HttpStatusCode.OK,
                        DataResult = Id,
                        MessageCode = MessageCode.NoContent
                    };
                }
            }
            catch (Exception ex)
            {
                return new CommonResultDto<TPrimaryKey>
                {
                    IsSuccessful = true,
                    ErrorMessage = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    DataResult = Id,
                    MessageCode = MessageCode.Exeption
                };
            }
        }

        public async Task<CommonResultDto<TEntityDto>> GetByIdAsync(TPrimaryKey Id)
        {
            try
            {
                var entity = await Repository.GetByIdAsync(Id);
                if (entity != null)
                {
                    return new CommonResultDto<TEntityDto>
                    {
                        IsSuccessful = true,
                        ErrorMessage = "",
                        StatusCode = System.Net.HttpStatusCode.OK,
                        DataResult = Factory.ObjectMapper.Map<TEntityDto>(entity)
                    };
                }
                else
                {
                    return new CommonResultDto<TEntityDto>
                    {
                        IsSuccessful = true,
                        ErrorMessage = "",
                        StatusCode = System.Net.HttpStatusCode.OK,
                        DataResult = Factory.ObjectMapper.Map<TEntityDto>(entity),
                        MessageCode = MessageCode.NoContent
                    };
                }
            }
            catch (Exception ex)
            {
                return new CommonResultDto<TEntityDto>
                {
                    IsSuccessful = true,
                    ErrorMessage = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    MessageCode = MessageCode.Exeption
                };
            }
        }

    }
}
