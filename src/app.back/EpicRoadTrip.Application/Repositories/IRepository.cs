using EpicRoadTrip.Domain.ErrorHandling;

namespace EpicRoadTrip.Application.Repositories;

public interface IRepository<T> where T : class
{
    public Task<Result<T>> Create(T entity, CancellationToken cancellationToken);

    public Task<Result<T>> Update(T entity, CancellationToken cancellationToken);

    public Task<Result<T>> Delete(T entity, CancellationToken cancellationToken);

    public Task<Result<T>> GetById(int id, CancellationToken cancellationToken);

    public Result<IEnumerable<T>> GetAll();
}