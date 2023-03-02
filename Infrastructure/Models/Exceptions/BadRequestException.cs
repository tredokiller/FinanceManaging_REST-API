namespace Infrastructure.Models.Exceptions;

public class BadRequestException : Exception
{
    public const string WrongIdMessage = "Invalid Id";
    public const string InvalidRequestMessage = "Invalid Data";
    
    private int StatusCode { get; } = 400;

    public BadRequestException(string message = InvalidRequestMessage) : base(message) { }
}