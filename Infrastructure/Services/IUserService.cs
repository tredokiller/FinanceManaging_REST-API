using Domain.Entities;
using Infrastructure.Services.Requests;

namespace Infrastructure.Services;

public interface IUserService
{
    Task<UserToken> Authenticate(UserAuthenticateRequest userInputData);
}