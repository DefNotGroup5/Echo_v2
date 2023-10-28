using System.Net.Security;
using Domain.Account.DTOs;
using Domain.Account.Models;

namespace Application.Account.LogicInterfaces;

public class UserLogic : IUserLogic
{
    private readonly string _accountDao;

    public UserLogic(string accountDao)
    {
        _accountDao = accountDao;
    }


    public async Task<User> Register(UserCreationDto dto)
    {
        /*
        User? user = await userDao.GetByUsernameAsync(userCreationDto.Username);
        if(user != null)
            throw new Exception("Username is already taken!");    
        */
        ValidateRegister(dto);
        
        User userToCreate = new User(dto.Username, dto.Password)
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = dto.Password,
            Address = dto.Address,
            City = dto.City,
            PostalCode = dto.PostalCode,
            Country = dto.City,
            IsSeller = dto.IsSeller
        };
        //User created = await userDao.CreateAsync(userToCreate);
        //return created;
        throw new NotImplementedException();
    }

    public Task Login(UserLoginDto dto)
    {
        ValidateLogin(dto);
        /*
        User? user = await userDao.GetByUsernameAsync(userLoginDto.Username);
        await userDao.Login(user);
         */
        throw new NotImplementedException();
    }

    private static void ValidateRegister(UserCreationDto dto)
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

    private static async void ValidateLogin(UserLoginDto dto)
    {
        /*
        User? user = await userDao.GetByUsernameAsync(userLoginDto.Username);
        if(user == null)
            throw new Exception("Username or password is incorrect!");
        if(!user.Password.Equals(userLoginDto.Password)
            throw new Exception("Username or Password is incorrect!");     
        */
    }
}