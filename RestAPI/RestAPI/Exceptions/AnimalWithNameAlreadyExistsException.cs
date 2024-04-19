namespace RestAPI.Exceptions;

public class AnimalWithNameAlreadyExistsException(string name) : ValidationException($"Animal with name '{name}' already exists") { }