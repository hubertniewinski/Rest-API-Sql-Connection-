using RestAPI.Exceptions;
using RestAPI.Models;
using RestAPI.Repositories;

namespace RestAPI.Services;

internal class AnimalsService(IAnimalsRepository animalsRepository) : IAnimalsService
{
	public async Task<IEnumerable<AnimalDto>> GetAnimalsAsync(string orderBy, CancellationToken cancellationToken) =>
		(await animalsRepository.GetAllAsync(orderBy, cancellationToken))
		.Select(x => new AnimalDto(x))
		.ToList();

	public async Task CreateAnimalAsync(CreateAnimalDto dto, CancellationToken cancellationToken)
	{
		var nameExists = (await animalsRepository.GetByNameAsync(dto.Name, cancellationToken)).Any();
		if (nameExists)
		{
			throw new AnimalWithNameAlreadyExistsException(dto.Name);
		}
		
		var model = Animal.Create(default, dto.Name, dto.Description, dto.Category, dto.Area);

		await animalsRepository.CreateAsync(model, cancellationToken);
	}

	public async Task UpdateAnimalAsync(int id, CreateAnimalDto dto, CancellationToken cancellationToken)
	{
		var nameExists = (await animalsRepository.GetByNameAsync(dto.Name, cancellationToken)).Any(x => x.IdAnimal != id);
		if (nameExists)
		{
			throw new AnimalWithNameAlreadyExistsException(dto.Name);
		}
		
		var animal = await animalsRepository.GetAsync(id, cancellationToken);

		animal.SetName(dto.Name);
		animal.SetDescription(dto.Description);
		animal.SetCategory(dto.Category);
		animal.SetArea(dto.Area);
		
		await animalsRepository.UpdateAsync(animal, cancellationToken);
	}

	public Task DeleteAnimalAsync(int id, CancellationToken cancellationToken) 
		=> animalsRepository.DeleteAsync(id, cancellationToken);
}