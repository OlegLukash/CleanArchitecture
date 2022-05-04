using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Application.Common.Models;
using OnlineBookShop.Application.Extensions;
using OnlineBookShop.Application.Repositories;
using OnlineBookShop.Domain;
using OnlineBookShop.Infrastructure.Persistance.Contexts;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace OnlineBookShop.Infrastructure.Persistance.Repositories
{
    public class EFCoreRepository : IRepository
    {
        private readonly OnlineBookShopDbContext _onlineBookShopDbContext;
        private readonly IMapper _mapper;

        public EFCoreRepository(OnlineBookShopDbContext onlineBookShopDbContext, IMapper mapper)
        {
            _onlineBookShopDbContext = onlineBookShopDbContext;
            _mapper = mapper;
        }

        public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity
        {
            return await _onlineBookShopDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById<TEntity>(int id) where TEntity : BaseEntity
        {
            return await _onlineBookShopDbContext.FindAsync<TEntity>(id);
        }

        public async Task<TEntity> GetByIdWithInclude<TEntity>(int id, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity
        {
            var query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _onlineBookShopDbContext.SaveChangesAsync();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _onlineBookShopDbContext
                .Set<TEntity>()
                .Add(entity);
        }

        public async Task<TEntity> Delete<TEntity>(int id) where TEntity : BaseEntity
        {
            var entity = await _onlineBookShopDbContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new ValidationException($"Object of type {typeof(TEntity)} with id { id } not found");
            }

            _onlineBookShopDbContext.Set<TEntity>().Remove(entity);

            return entity;
        }

        public async Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : BaseEntity
                                                                                                    where TDto : class
        {
            return await _onlineBookShopDbContext.Set<TEntity>().CreatePaginatedResultAsync<TEntity, TDto>(pagedRequest, _mapper);
        }

        private IQueryable<TEntity> IncludeProperties<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity
        {
            IQueryable<TEntity> entities = _onlineBookShopDbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
    }
}
