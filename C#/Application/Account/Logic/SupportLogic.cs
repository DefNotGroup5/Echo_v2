using Application.Account.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Shopping.Models;
using GrpcClientServices.Services;
using Microsoft.Extensions.Configuration;

namespace Application.Account.Logic;

public class SupportLogic : ISupportLogic
{
    private SupportService _supportService;
    private UsersService _usersService;

    public SupportLogic(SupportService supportService, UsersService usersService)
    {
        _supportService = supportService;
        _usersService = usersService;
    }

    public async Task<Message?> RequestSupportAsync(MessageRequestDto dto)
    {
        try
        {
            string? validation = ValidateRequestDto(dto).Result;
            if (!string.IsNullOrEmpty(validation))
            {
                throw new Exception(validation);
            }
            Message message = new Message(dto.CustomerId, dto.Request);
            Message? returnMessage = await _supportService.RequestSupportAsync(message);
            return returnMessage;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    public async Task ProvideSupportAsync(MessageResponseDto dto)
    {
        try
        {
            string? validation = ValidateResponseDto(dto).Result;
            if (!string.IsNullOrEmpty(validation))
            {
                throw new Exception(validation);
            }
            Message found = null;
            ICollection<Message>? messages = await _supportService.GetAllAsync();
            if (messages != null)
                foreach (var message in messages)
                {
                    if (message.Id == dto.MessageId)
                    {
                        found = message;
                    }
                }
            if (found is null)
            {
                throw new Exception("Message was not found!");
            }
            found.Response = dto.Response;
            found.Answered = true;
            await _supportService.ProvideSupportAsync(found);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task<ICollection<Message>?> GetAllAsync()
    {
        return await _supportService.GetAllAsync();
    }

    public async Task<string?> ValidateRequestDto(MessageRequestDto dto)
    {
        string validation = "";
        User? user = await _usersService.GetByIdAsync(dto.CustomerId);
        if (user == null)
        {
            validation = "User does not exist!";
            return validation;
        }
        if (string.IsNullOrEmpty(dto.Request))
        {
            validation = "Support message cannot be empty!";
            return validation;
        }
        return validation;
    }

    public async Task<string?> ValidateResponseDto(MessageResponseDto dto)
    {
        string validation = "";

        if (string.IsNullOrEmpty(dto.Response))
        {
            validation = "Response cannot be empty! Be a better admin";
            return validation;
        }
        return validation;
    }
}