namespace RestAPI.Models;

public record AnimalDto(int AnimalId, string Name, string? Description, string Category, string Area)
{
    public AnimalDto(Animal animal) : this(animal.IdAnimal, animal.Name, animal.Description, animal.Category, animal.Area) { }
}