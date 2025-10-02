using Microsoft.EntityFrameworkCore;
using VitalScope.Common.Options;
using VitalScope.Insfrastructure.Specifications.Base;

namespace VitalScope.Insfrastructure.Repositories.Base;

 public abstract class BaseRepository<TContext, TEntity> : IRepository<TEntity>
               where TEntity : class
               where TContext : DbContext
    {
        protected BaseRepository(TContext context)
        {
            Context = context;
            Options = RepositoryOptions.DefaultValue;
        }

        protected TContext Context { get; }

        public RepositoryOptions Options { get; }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ThrowIfArgumentIsEmpty(entity);

            var result = Context.Set<TEntity>().Add(entity);

            if (Options.AutoSaveEnabled)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }

            return result.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            ThrowIfArgumentIsEmpty(entities);

            Context.Set<TEntity>().AddRange(entities);

            if (Options.AutoSaveEnabled)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ThrowIfArgumentIsEmpty(entity);

            var removed = Context.Set<TEntity>()
                .Remove(entity);

            if (Options.AutoSaveEnabled)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }

            return removed.Entity;
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            ThrowIfArgumentIsEmpty(entities);

            Context.Set<TEntity>().RemoveRange(entities.ToArray());

            if (Options.AutoSaveEnabled)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ThrowIfArgumentIsEmpty(entity);

            Context.Entry(entity).State = EntityState.Modified;

            if (Options.AutoSaveEnabled)
            {
                await Context.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }

        public Task<int> CountAsync(ISpecification<TEntity> specification = default, CancellationToken cancellationToken = default)
        {
            return specification != default
                ? CreateQuery(specification).CountAsync(cancellationToken)
                : Context.Set<TEntity>().CountAsync(cancellationToken);
        }

        public Task<TEntity> GetFirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            ThrowIfArgumentIsEmpty(specification);

            return CreateQuery(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            ThrowIfArgumentIsEmpty(specification);

            return await CreateQuery(specification).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            ThrowIfArgumentIsEmpty(specification);

            return await CreateQuery(specification).Skip(specification.Offset)
                .Take(specification.Count)
                .ToListAsync(cancellationToken);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) => Context.SaveChangesAsync(cancellationToken);

        #region Private Methods

        private static void ThrowIfArgumentIsEmpty<TArgument>(TArgument argument)
        {
            if (EqualityComparer<TArgument>.Default.Equals(argument, default))
            {
                throw new ArgumentNullException(nameof(argument), $"Переданный параметр не может быть равен null.");
            }

            if (argument is IEnumerable<TEntity> enumerable && !enumerable.Any())
            {
                throw new ArgumentException("Коллекция не должна быть пустой.");
            }
        }

        private IQueryable<TEntity> CreateQuery(ISpecification<TEntity> specification)
        {
            var query = Context.Set<TEntity>()
                .AsQueryable();

            if (!specification.Trackable)
            {
                query = query.AsNoTracking();
            }

            query = specification.Includes.Aggregate(query, (querable, includeTo) => querable = includeTo(querable));

            if (specification.Criteria != default)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Orderes.Aggregate(query, (querable, includeTo) => querable = includeTo(querable));

            return query;
        }

        #endregion Private Methods
    }