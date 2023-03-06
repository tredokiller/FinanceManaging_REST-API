using Infrastructure.Models.Exceptions;
using Infrastructure.Models.Requests;
using Infrastructure.Services;
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

    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Data</response>
    /// <summary>
    /// User Authentication
    /// </summary>
    /// <remarks>
    /// Authentication example
    /// 
    ///     POST /Authenticate
    ///     {
    ///        "name": "Slava",
    ///        "password": "robot2344"
    ///     }
    ///     
    /// </remarks>
    [AllowAnonymous]
    [HttpPost]
    [Route("Authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Authenticate(UserAuthenticateRequest userData)
    {
        if (userData == null)
        {
            throw new BadRequestException();
        }

        var token = _userService.Authenticate(userData);

        if (token.Result == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }
}