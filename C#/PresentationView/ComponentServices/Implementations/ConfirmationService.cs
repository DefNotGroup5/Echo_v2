using System;
using PresentationView.ComponentServices.Interfaces;

namespace PresentationView.ComponentServices.Implementations;

public class ConfirmationService : IConfirmationService
{
    public event Action<string, string, Action<bool>>? OnConfirmationRequested;
    public void ShowConfirmation(string title, string description, Action<bool> callback)
    {
        OnConfirmationRequested?.Invoke(title, description, callback);
    }
}