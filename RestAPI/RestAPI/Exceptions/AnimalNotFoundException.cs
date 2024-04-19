namespace RestAPI.Exceptions;

public class AnimalNotFoundException(int id) : NotFoundException($"Animal with id '{id}' not found") { }