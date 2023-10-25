using System.Net.Security;

namespace Application.Account.LogicInterfaces;

public class UserLogic : IUserLogic
{
    private readonly string _accountDao;

    public UserLogic(string accountDao)
    {
        _accountDao = accountDao;
    }


    public async Task<string> Register(string userCreationDto)
    {
        /*
        User? user = await userDao.GetByUsernameAsync(userCreationDto.Username);
        if(user != null)
            throw new Exception("Username is already taken!");    
        */
        ValidateRegister(userCreationDto);
        /*
        User userToCreate = new User(userCreationDto.Whatever);
        User created = await userDao.CreateAsync(userToCreate);
        return created; 
         */
        throw new NotImplementedException();
    }

    public Task Login(string userLoginDto)
    {
        ValidateLogin(userLoginDto);
        /*
        User? user = await userDao.GetByUsernameAsync(userLoginDto.Username);
        await userDao.Login(user);
         */
        throw new NotImplementedException();
    }

    private static void ValidateRegister(string userCreationDto)
    {
        /*
        if(string.IsNullOrEmpty(Email))
            throw new Exception("Email cannot be empty!");
        if(string.IsNullOrEmpty(FirstName))
            throw new Exception("First Name cannot be empty!")
        if(string.IsNullOrEmpty(LastName))
            throw new Exception("Last Name cannot be empty!")
        if(string.IsNullOrEmpty(Password))
            throw new Exception("Password cannot be empty!")
        if(Password.Length < 8)
            throw new Exception("Password must contain at least 8 characters!");
        if(string.IsNullOrEmpty(Address))
            throw new Exception("Address cannot be empty!");
        if(string.IsNullOrEmpty(City))
            throw new Exception("City cannot be empty!"));
        if(PostalCode < 0 || > 9999999999)
            throw new Exception("Postal code is invalid!");
        if(IsSeller)
            if(ConfirmationForm == null)
                throw new Exception("No Confirmation Form uploaded!");                         
         */
    }

    private static async void ValidateLogin(string userLoginDto)
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