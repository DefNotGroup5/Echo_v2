using Domain.Account.DTOs;
using Domain.Account.Models;

namespace Application.Account.LogicInterfaces;

public interface IUserLogic
{
    Task<User> Register(UserCreationDto userCreationDto);
    Task Login(UserLoginDto userLoginDto);
}