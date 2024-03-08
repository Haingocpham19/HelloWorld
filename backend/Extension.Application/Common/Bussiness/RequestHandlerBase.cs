using Extension.Application.AppFactory;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace newPSG.PMS.AppCore.Common.Business
{
    public class RequestHandlerBase
    {
        #region Lazy load IAppFactory
        public IServiceProvider ServiceProvider { get; set; }
        public IAppFactory AppFactory => ServiceProvider.GetService<IAppFactory>();
        #endregion

        #region Prop

        public Task<TResponse> SendCqrsRequest<TResponse>(IRequest<TResponse> request)
        {
            return AppFactory.SendCqrsRequest(request);
        }

        #endregion

        public IQueryable<TEntity> GetEntityQueryable<TPrimaryKey, TEntity>()
            where TEntity : class
        {
            var repo = AppFactory.Repository< TPrimaryKey, TEntity> ();
            return repo.GetAll();
        }
    }
}
