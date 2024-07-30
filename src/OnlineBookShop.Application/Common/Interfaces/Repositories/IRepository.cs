using OnlineBookShop.Application.Common.Models;
using OnlineBookShop.Domain;
using System.Linq.Expressions;

namespace OnlineBookShop.Application.Common.Interfaces.Repositories
{
    public interface IRepository
    {
        Task<TEntity> GetById<TEntity>(int id) where TEntity : BaseEntity;

        Task<TEntity> GetByIdWithInclude<TEntity>(int id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity;

        Task<List<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity;

        void Add<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task<TEntity> Delete<TEntity>(int id) where TEntity : BaseEntity;

        Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : BaseEntity
                                                                                             where TDto : class;
        Task<TEntity> FindByAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;
       
    }
}
