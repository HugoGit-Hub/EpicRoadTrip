using EpicRoadTrip.Application.Repositories;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Infrastructure.Context;

namespace EpicRoadTrip.Infrastructure.Repositories;

public class Repository<T>(EpicRoadTripContext context) : IRepository<T> where T : class
{
    public async Task<Result<T>> Create(T entity, CancellationToken cancellationToken)
    {
        try
        {
            var create = await context.Set<T>().AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Result<T>.Success(create.Entity);
        }
        catch (Exception e)
        {
            return Result<T>.Failure(RepositoryErrors.FailedToCreateError(e));
        }
    }

    public async Task<Result<T>> Update(T entity, CancellationToken cancellationToken)
    {
        try
        {
            var update = context.Set<T>().Update(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result<T>.Success(update.Entity);
        }
        catch (Exception e)
        {
            return Result<T>.Failure(RepositoryErrors.FailedToUpdateError(e));
        }
    }

    public async Task<Result<T>> Delete(T entity, CancellationToken cancellationToken)
    {
        try
        {
            var delete = context.Set<T>().Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result<T>.Success(delete.Entity);
        }
        catch (Exception e)
        {
            return Result<T>.Failure(RepositoryErrors.FailedToDeleteError(e));
        }
    }

    public async Task<Result<T>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await context.Set<T>().FindAsync([id], cancellationToken);
            
            return entity == null 
                ? Result<T>.Failure(RepositoryErrors.FailedToGetByIdError()) 
                : Result<T>.Success(entity);
        }
        catch (Exception e)
        {
            return Result<T>.Failure(RepositoryErrors.FailedToGetByIdError(e));
        }
    }

    public Result<IEnumerable<T>> GetAll()
    {
        try
        {
            var entities = context.Set<T>().AsEnumerable();

            return Result<IEnumerable<T>>.Success(entities);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<T>>.Failure(RepositoryErrors.FailToGetAllError(e));
        }
    }
}