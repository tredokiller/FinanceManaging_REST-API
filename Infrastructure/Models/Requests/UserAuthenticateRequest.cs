using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.Requests;

public class UserAuthenticateRequest
{
    [Required]
    public string Name { set; get; }
    
    [Required]
    public string Password { set; get; }
}