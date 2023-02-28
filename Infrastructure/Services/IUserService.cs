using Domain.Entities;
using Infrastructure.Models;
using Infrastructure.Models.Requests;

namespace Infrastructure.Services;

public interface IUserService
{
    Task<UserToken> Authenticate(UserAuthenticateRequest userInputData);
}