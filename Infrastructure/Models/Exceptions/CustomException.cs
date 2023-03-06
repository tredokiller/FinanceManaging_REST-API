using System.Net;

namespace Infrastructure.Models.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        public CustomException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}