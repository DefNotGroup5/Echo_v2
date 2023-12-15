using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Account.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using GrpcClientServices.Services;
using Microsoft.AspNetCore.Http.HttpResults;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Account_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IConfiguration _config;

    public UsersController(IConfiguration config, IUserLogic userLogic)
    {
        _config = config;
        _userLogic = userLogic;
    }
    private List<Claim> GenerateClaims(User user)
    {
        bool isSeller = false;
        bool isAuthorizedSeller = false;
        if (user is Seller seller)
        {
            isSeller = true;
            if (seller.IsAuthorized)
            {
                isAuthorizedSeller = true;
            }
        }
        bool isAdmin = user is Admin;
        bool isCustomer = !isAdmin && !isSeller;
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"] ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.StreetAddress, user.Address),
            new Claim("City", user.City),
            new Claim(ClaimTypes.Country, user.Country),
            new Claim(ClaimTypes.PostalCode, user.PostalCode.ToString()),
            new Claim("IsSeller", isSeller.ToString()),
            new Claim("IsAdmin", isAdmin.ToString()),
            new Claim("IsAuthorizedSeller", isAuthorizedSeller.ToString()),
            new Claim("IsCustomer", isCustomer.ToString())
        };
        return claims.ToList();
    }
    
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    


    [HttpPost("Register")]
    public async Task<ActionResult<User>> Register(UserCreationDto userCreationDto)
    {
        try
        {
            User? user = await _userLogic.Register(userCreationDto);
            if (user != null) 
                return Created($"/Users/{user.Id}", user);
            throw new Exception("Error registering User!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch("Login")]
    public async Task<ActionResult> Login(UserLoginDto userLoginDto)
    {
        try
        {
            Console.WriteLine("Reached");
            User? user = await _userLogic.Login(userLoginDto);
            string token = "";
            if (user != null)
            {
                token = GenerateJwt(user);
            }
            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<UserTransferDto>>> GetAllAsync()
    {
        try
        {
            ICollection<UserTransferDto> transferredUsers = new List<UserTransferDto>();
            ICollection<User?> users = await _userLogic.GetAll();
            foreach (var user in users)
            {
                UserTransferDto dto;
                if (user is Seller seller)
                {
                    dto = new UserTransferDto()
                    {
                        User = user,
                        IsSeller = true,
                        IsAdmin = false,
                        IsAuthorized = seller.IsAuthorized
                    };
                }
                else if (user is Admin)
                { 
                    dto = new UserTransferDto()
                    {
                        User = user,
                        IsSeller = false,
                        IsAdmin = true,
                        IsAuthorized = false
                    };
                }
                else
                { 
                    dto = new UserTransferDto()
                    {
                        User = user,
                        IsSeller = false,
                        IsAdmin = false,
                        IsAuthorized = false
                    };
                }
                transferredUsers.Add(dto);
            }
            return Ok(transferredUsers);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserTransferDto>> GetById([FromRoute] int id)
    {
        try
        {
            UserTransferDto dto = null;
            ICollection<User?> users = await _userLogic.GetAll();
            foreach (var user in users)
            {
                if (user is Seller seller)
                {
                    dto = new UserTransferDto()
                    {
                        User = user,
                        IsSeller = true,
                        IsAdmin = false,
                        IsAuthorized = seller.IsAuthorized
                    };
                }
                else if (user is Admin)
                { 
                    dto = new UserTransferDto()
                    {
                        User = user,
                        IsSeller = false,
                        IsAdmin = true,
                        IsAuthorized = false
                    };
                }
                else
                { 
                    dto = new UserTransferDto()
                    {
                        User = user,
                        IsSeller = false,
                        IsAdmin = false,
                        IsAuthorized = false
                    };
                }
            }
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}