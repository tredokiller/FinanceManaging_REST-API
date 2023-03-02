namespace Infrastructure.Models.Exceptions;

public class NotFoundException : Exception
{
    public const string DataNotFoundMessage = "Data is not founded";
    public int StatusCode { get; } = 404;

    public NotFoundException(string message = DataNotFoundMessage) : base(message) { }
}
