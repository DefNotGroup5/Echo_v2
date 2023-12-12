namespace Domain.Shopping.Models;

public class Message
{
    public int CustomerId { get; set; }
    public bool Answered { get; set; }
    public string Request { get; set; }
    public string Response { get; set; }
    public int Id { get; set; }
    
    public Message(int customerId, string request)
    {
        CustomerId = customerId;
        Request = request;
        Answered = false;
    }
}