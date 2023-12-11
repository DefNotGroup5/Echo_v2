namespace Domain.Shopping.Models;

public class Message
{
    public int customerId { get; set; }
    public bool answered { get; set; }
    public string request { get; set; }
    public string response { get; set; }
    public int id { get; set; }
    
    public Message(int customerId, string request)
    {
        this.customerId = customerId;
        this.request = request;
        answered = false;
    }
}