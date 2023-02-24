namespace Infrastructure.Services.Requests;

public class UserAuthenticateRequest
{
    public string Name { set; get; }
    public string Password { set; get; }
}