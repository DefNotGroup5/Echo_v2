using Application.Shopping.Logic;
using Application.Shopping.LogicInterfaces;
using Domain.Auth;
using GrpcClientServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartLogic, ShoppingCartLogic>();
builder.Services.AddScoped<IItemLogic, ItemLogic>();
builder.Services.AddScoped<IOrderLogic, OrderLogic>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<IReviewLogic, ReviewLogic>();
builder.Services.AddScoped<WishlistService>();
builder.Services.AddScoped<IWishlistLogic, WishlistLogic>();
AuthorizationPolicies.AddPolicies(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();