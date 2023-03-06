using System.Net;

namespace Infrastructure.Models.Exceptions;

public class BadRequestException : CustomException
{
    public const string WrongIdMessage = "Invalid Id";
    public const string InvalidRequestMessage = "Invalid Data";
    
    public BadRequestException(string message = InvalidRequestMessage) : base(message , HttpStatusCode.BadRequest) { }
}