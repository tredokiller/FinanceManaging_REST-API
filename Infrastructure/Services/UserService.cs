using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _usersRepository;
    private readonly IConfiguration _configuration;
    
    public UserService(IRepository<User> usersRepository ,IConfiguration configuration)
    {
        _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    
    public Task<UserToken> Authenticate(UserAuthenticateRequest userInputData)
    {
        if (userInputData == null)
        {
            throw new ArgumentNullException(nameof(userInputData));
        }
        
        if (!_usersRepository.GetAll().Result.Any(user => user.Name == userInputData.Name && user.Password == userInputData.Password))
        {
            return Task.FromResult<UserToken>(null);
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userInputData.Name)                    
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(new UserToken { Token = tokenHandler.WriteToken(token) });
    }
}