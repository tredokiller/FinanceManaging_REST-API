using Infrastructure.Services;
using Infrastructure.Services.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[Authorize]
[Route("api/[controller]")]
[ApiController]  
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    
    public AuthenticationController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }
    
    
    [AllowAnonymous]
    [HttpPost]
    [Route("Authenticate")]
    public IActionResult Authenticate(UserAuthenticateRequest userData)
    {
        if (userData == null)
        {
            throw new ArgumentNullException(nameof(userData));
        }
        var token = _userService.Authenticate(userData);

        if (token.Result == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
    
}