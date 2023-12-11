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
            string validated = await ValidateRegister(dto);
            if (!string.IsNullOrEmpty(validated))
                throw new Exception(validated);
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
                    Country = dto.Country,
                    IsSeller = dto.IsSeller,
                };
            }
            if (dto.IsAdmin)
            {
                userToCreate = new Admin(dto.Email, dto.Password)
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Password = dto.Password,
                    Address = dto.Address,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    Country = dto.Country,
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
                    Country = dto.Country,
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
            string validated = await ValidateLogin(dto);
            if (!string.IsNullOrEmpty(validated))
            {
                throw new Exception(validated);
            }
            User? user = await _usersService.GetByEmailAsync(dto.Email);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<string> ValidateRegister(UserCreationDto dto)
    {
        string validated = "";
        User? user = await _usersService.GetByEmailAsync(dto.Email);
        if (user != null)
        {
            validated = "User with such email already exists!";
            return validated;
        }
        if(string.IsNullOrEmpty(dto.Email))
        {
            validated = "Email cannot be empty!";
            return validated;
        }
        if (string.IsNullOrEmpty(dto.FirstName))
        {
            validated = "First Name cannot be empty!";
            return validated;
        }
        if (string.IsNullOrEmpty(dto.LastName))
        {
            validated = "Last Name cannot be empty!";
            return validated;
        }
        if (string.IsNullOrEmpty(dto.Password))
        {
            validated = "Password cannot be empty!";
            return validated;
        }
        if(dto.Password.Length < 8)
        {
            validated = "Password must contain at least 8 characters!";
            return validated;
        }
        if(string.IsNullOrEmpty(dto.Address))
        {
            validated = "Address cannot be empty!";
            return validated;
        }
        if(string.IsNullOrEmpty(dto.City))
        {
            validated = "City cannot be empty!";
            return validated;
        }
        if(dto.PostalCode < 0 || dto.PostalCode > 9999999)
        {
            validated = "Postal code is invalid!";
            return validated;
        }
        if(string.IsNullOrEmpty(dto.Country))
        {
            validated = "Country cannot be empty";
            return validated;
        }
        return validated;
    }

    private async Task<string> ValidateLogin(UserLoginDto dto)
    {
        string validated = "";
        User? user = await _usersService.GetByEmailAsync(dto.Email);
        if(user == null)
            validated = "Username or password is incorrect!";
        if(user != null && !user.Password.Equals(dto.Password))
            validated = "Username or Password is incorrect!";
        return validated;
    }
}