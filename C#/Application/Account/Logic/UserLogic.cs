﻿using Domain.Account.Models;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
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
                    Country = dto.Country
                };
            }
            if(dto is { IsAdmin: false, IsSeller: false })
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

    public async Task<ICollection<User?>> GetAll()
    {
        try
        {
            ICollection<User?> users = await _usersService.GetAllUsersAsync();
            foreach (var user in users)
            {
                if (user is Seller)
                {
                    Console.WriteLine("true");
                }
                else
                {
                    Console.WriteLine("false");
                }
            }
            return users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> GetById(int id)
    {
        try
        {
            return await _usersService.GetByIdAsync(id);
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
            validated = "Email or password is incorrect!";
        if(user != null && !user.Password.Equals(dto.Password))
            validated = "Email or Password is incorrect!";
        return validated;
    }
}