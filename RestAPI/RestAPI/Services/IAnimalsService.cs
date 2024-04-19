using RestAPI.Models;

namespace RestAPI.Services;

public interface IAnimalsService
{
    Task<IEnumerable<AnimalDto>> GetAnimalsAsync(string orderBy, CancellationToken cancellationToken);
    Task CreateAnimalAsync(CreateAnimalDto dto, CancellationToken cancellationToken);
    Task UpdateAnimalAsync(int id, CreateAnimalDto dto, CancellationToken cancellationToken);
    Task DeleteAnimalAsync(int id, CancellationToken cancellationToken);
}