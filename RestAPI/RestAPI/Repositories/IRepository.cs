namespace RestAPI.Repositories;

public interface IRepository<T>
{
	Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
	Task<T> GetAsync(int id, CancellationToken cancellationToken = default);
	Task CreateAsync(T model, CancellationToken cancellationToken = default);
	Task UpdateAsync(T model, CancellationToken cancellationToken = default);
	Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}