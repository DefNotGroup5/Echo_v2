using System.Net.Security;
using Application.Account.DaoInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;

namespace Application.Account.LogicInterfaces;

public class UserLogic : IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(IUserDao userDao)
    {
        _userDao = userDao;
    }
    
    public async Task<User> Register(UserCreationDto dto)
    {
        
        User? user = await _userDao.GetByEmailAsync(dto.Email);
        if(user != null)
            throw new Exception("Username is already taken!");    
        
        ValidateRegister(dto);
        
        User userToCreate = new User(dto.Email, dto.Password)
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = dto.Password,
            Address = dto.Address,
            City = dto.City,
            PostalCode = dto.PostalCode,
            Country = dto.City,
            IsSeller = dto.IsSeller
        };
        User created = await _userDao.CreateAsync(userToCreate);
        return created;
    }

    public async Task Login(UserLoginDto dto)
    {
        ValidateLogin(dto);
        
        User? user = await _userDao.GetByEmailAsync(dto.Email);
        if (user != null) await _userDao.Login(user);
    }

    public async Task Logout(UserLoginDto dto)
    {
        User? existing = await _userDao.GetByEmailAsync(dto.Email);
        if (existing == null)
            throw new Exception("A user with this username does not exist!");
        await _userDao.Logout(existing);
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
        User? user = await _userDao.GetByEmailAsync(dto.Email);
        if(user == null)
            throw new Exception("Username or password is incorrect!");
        if(!user.Password.Equals(dto.Password))
            throw new Exception("Username or Password is incorrect!");
    }
}