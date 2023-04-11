using Blazored.LocalStorage;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUI.Pages.Logging;

public class LoginBase : ComponentBase
{
    [Inject]
    protected IUserService UserService { get; set; }
    
    [Inject] 
    protected ILocalStorageService LocalStorageService { get; set; }
    
    [Inject] 
    protected NavigationManager NavigationManager { get; set; }

    [Inject] 
    protected AuthenticationStateProvider AuthStateProvider { get; set; }
    
}