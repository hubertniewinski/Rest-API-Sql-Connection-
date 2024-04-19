namespace RestAPI.Exceptions;

public class NotFoundException(string message) : Exception(message);