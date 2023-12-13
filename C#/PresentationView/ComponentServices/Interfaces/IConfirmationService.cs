namespace PresentationView.ComponentServices.Interfaces;

public interface IConfirmationService
{
    event Action<string, string, Action<bool>> OnConfirmationRequested;
    void ShowConfirmation(string title, string description, Action<bool> callback);
}