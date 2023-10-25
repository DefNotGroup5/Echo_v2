namespace Application.Account.LogicInterfaces;

public interface IUserLogic
{
    Task<string> Register(string userCreationDto);
    Task Login(string userLoginDto);
}