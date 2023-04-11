using Blazored.LocalStorage;
using BlazorUI.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using DateReportService = BlazorUI.Services.DateReportService;
using FinanceOperationService = BlazorUI.Services.FinanceOperationService;
using UserService = BlazorUI.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7116/")});

builder.Services.AddScoped<IFinanceOperationService, FinanceOperationService>();
builder.Services.AddScoped<IIncomeExpenseService, IncomeExpensesService>();
builder.Services.AddScoped<IDateReportService, DateReportService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();