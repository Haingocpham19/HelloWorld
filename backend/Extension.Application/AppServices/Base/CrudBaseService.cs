using AutoMapper;
using AutoMapper.Internal.Mappers;
using Extension.Application.AppFactory;
using Extension.Application.Dto.Base;
using Extension.Application.Utilities;
using Extension.Domain.Common;
using Extension.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static Extension.Domain.Enum.ExtensionEnums;

namespace Extension.Services
{
    public class CrudBaseService<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput>
        where TEntity : class
        where TEntityDto : class
        where TPagedInput : PagedFullInputDto
        where TPagedOutput : class//, IEntity<TPrimaryKey>
    {
        public readonly IAppFactory Factory;
        public readonly IBaseRepository<TPrimaryKey, TEntity> Repository;
        public readonly IMapper ObjectMapper;

        public CrudBaseService(IAppFactory factory)
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
                var result = await Repository.InsertAsync(entity);

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
                        ErrorMessage = string.Empty,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        DataResult = Factory.ObjectMapper.Map<TEntityDto>(entity)
                    };
                }
                else
                {
                    return new CommonResultDto<TEntityDto>
                    {
                        IsSuccessful = false,
                        ErrorMessage = EnumExtensionMethods.GetEnumDescription(MessageCode.NotFound),
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

        public async Task<CommonResultDto<TEntityDto>> DeleteById(TPrimaryKey Id)
        {
            try
            {
                var entity = await Repository.DeleteAsync(Id);
                if (entity != null)
                {
                    return new CommonResultDto<TEntityDto>
                    {
                        IsSuccessful = true,
                        ErrorMessage = string.Empty,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        DataResult = Factory.ObjectMapper.Map<TEntityDto>(entity)
                    };
                }
                else
                {
                    return new CommonResultDto<TEntityDto>
                    {
                        IsSuccessful = false,
                        ErrorMessage = EnumExtensionMethods.GetEnumDescription(MessageCode.NotFound),
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

        public async Task<CommonResultDto<IQueryable<TPagedOutput>>> GetAllQueryableAsync(TPagedInput input)
        {
            try
            {
                var entity =  Repository.GetAll();
                if (entity != null)
                {
                    return new CommonResultDto<IQueryable<TPagedOutput>>
                    {
                        IsSuccessful = true,
                        ErrorMessage = string.Empty,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        DataResult = Factory.ObjectMapper.Map<IQueryable<TPagedOutput>>(entity)
                    };
                }
                else
                {
                    return new CommonResultDto<IQueryable<TPagedOutput>>
                    {
                        IsSuccessful = false,
                        ErrorMessage = EnumExtensionMethods.GetEnumDescription(MessageCode.NotFound),
                        StatusCode = System.Net.HttpStatusCode.OK,
                        DataResult = Factory.ObjectMapper.Map<IQueryable<TPagedOutput>>(entity),
                        MessageCode = MessageCode.NoContent
                    };
                }
            }
            catch (Exception ex)
            {
                return new CommonResultDto<IQueryable<TPagedOutput>>
                {
                    IsSuccessful = false,
                    ErrorMessage = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    MessageCode = MessageCode.Exeption
                };
            }
        }
    }
}
