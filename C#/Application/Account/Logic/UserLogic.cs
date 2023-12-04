using Application.Account.DaoInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;
using GrpcClientServices.Services;

namespace Application.Account.LogicInterfaces;

public class UserLogic : IUserLogic
{
    private readonly UsersService _usersService;

    public UserLogic(UsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task<User?> Register(UserCreationDto dto)
    {
        try
        {
            User? user = await _usersService.GetByEmailAsync(dto.Email);
            if(user != null)
                throw new Exception("Email is already taken!");    
        
            ValidateRegister(dto);
            User userToCreate = null;
            if (dto.IsSeller)
            {
                userToCreate = new Seller(dto.Email, dto.Password)
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Password = dto.Password,
                    Address = dto.Address,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    Country = dto.City,
                };
            }
            else
            {
                userToCreate = new Customer(dto.Email, dto.Password)
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Password = dto.Password,
                    Address = dto.Address,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    Country = dto.City,
                };
            }

            await _usersService.AddAsync(userToCreate);
            User? created = await _usersService.GetByEmailAsync(dto.Email);
            return created;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> Login(UserLoginDto dto)
    {
        try
        {
            ValidateLogin(dto);
            User? user = await _usersService.GetByEmailAsync(dto.Email);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void ValidateRegister(UserCreationDto dto)
    {
        if(string.IsNullOrEmpty(dto.Email))
            throw new Exception("Email cannot be empty!");
        if (string.IsNullOrEmpty(dto.FirstName))
            throw new Exception("First Name cannot be empty!");
        if (string.IsNullOrEmpty(dto.LastName))
            throw new Exception("Last Name cannot be empty!");
        if (string.IsNullOrEmpty(dto.Password))
            throw new Exception("Password cannot be empty!");
        if(dto.Password.Length < 8)
            throw new Exception("Password must contain at least 8 characters!");
        if(string.IsNullOrEmpty(dto.Address))
            throw new Exception("Address cannot be empty!");
        if(string.IsNullOrEmpty(dto.City))
            throw new Exception("City cannot be empty!");
        if(dto.PostalCode < 0 || dto.PostalCode > 9999999)
            throw new Exception("Postal code is invalid!");
        if(string.IsNullOrEmpty(dto.Country))
            throw new Exception("Country cannot be empty");
    }

    private async void ValidateLogin(UserLoginDto dto)
    {
        User? user = await _usersService.GetByEmailAsync(dto.Email);
        if(user == null)
            throw new Exception("Username or password is incorrect!");
        if(!user.Password.Equals(dto.Password))
            throw new Exception("Username or Password is incorrect!");
    }
}