using RestAPI.Models;

namespace RestAPI.Repositories;

public interface IAnimalsRepository : IRepository<Animal>
{
    Task<IEnumerable<Animal>> GetAllAsync(string orderBy, CancellationToken cancellationToken = default);
    Task<IEnumerable<Animal>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}