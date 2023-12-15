using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace Application.Account.LogicInterfaces;

public interface IUserLogic
{
    Task<User?> Register(UserCreationDto userCreationDto);
    Task<User?> Login(UserLoginDto dto);
    Task<ICollection<User?>> GetAll();
    Task<User?> GetById(int id);
}