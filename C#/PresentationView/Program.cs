using Domain.Auth;
using HTTPClients.ClientInterfaces;
using HTTPClients.Implementations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.OutputCaching;
using PresentationView.Auth;
using PresentationView.ComponentServices.Implementations;
using PresentationView.ComponentServices.Interfaces;
using PresentationView.Pages.Components;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IItemService, ItemHttpClient>();
builder.Services.AddScoped<IUserService, UserHttpClient>();
builder.Services.AddScoped<IItemService, ItemHttpClient>();
builder.Services.AddScoped<IAdminService, AdminHttpClient>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartHttpClient>();
builder.Services.AddScoped<IConfirmationService, ConfirmationService>();
builder.Services.AddScoped(
    sp => 
        new HttpClient { 
            BaseAddress = new Uri("http://localhost:5105") 
        }
);
builder.Services.AddScoped(
    sp => 
        new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5111")
        }
);

builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
AuthorizationPolicies.AddPolicies(builder.Services);
string url = "https://jfroxdaztgabtnnttaou.supabase.co/";
string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Impmcm94ZGF6dGdhYnRubnR0YW91Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MDEzODY3MDAsImV4cCI6MjAxNjk2MjcwMH0.UZgEug8wJOZG_6KkiZtZcQGe7vkp5PmhRNcVhKL8Kg4";
builder.Services.AddSingleton(new Supabase.Client(url, key));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();