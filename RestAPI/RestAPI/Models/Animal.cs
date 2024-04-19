namespace RestAPI.Models;

public class Animal
{
	public int IdAnimal { get; private set; }
	public string Name { get; private set; }
	public string? Description { get; private set; }
	public string Category { get; private set; }
	public string Area { get; private set; }

	public static Animal Create(int idAnimal, string name, string? description, string category, string area) =>
		new(idAnimal, name, description, category, area);

	private Animal(int idAnimal, string name, string? description, string category, string area)
	{
		IdAnimal = idAnimal;
		Name = name;
		Description = description;
		Category = category;
		Area = area;
	}

	public void SetName(string name) => Name = name;

	public void SetDescription(string? description) => Description = description;

	public void SetCategory(string category) => Category = category;

	public void SetArea(string area) => Area = area;
}