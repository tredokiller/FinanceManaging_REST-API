namespace Infrastructure.Models.Exceptions;

public class InvalidJwtTokenException : CustomException
{
    private const string InvalidRequestMessage = "Invalid JWT Token";
    
    public InvalidJwtTokenException(string message = InvalidRequestMessage) : base(message) { }
}