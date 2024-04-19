namespace RestAPI.Exceptions;

public class ValidationException(string message) : Exception(message);