using Infrastructure.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages.Logging;

public class LoginBase : ComponentBase
{
    [Inject]
    protected IUserService UserService { get; set; }


    [Inject] 
    protected NavigationManager NavigationManager { get; set; }
}