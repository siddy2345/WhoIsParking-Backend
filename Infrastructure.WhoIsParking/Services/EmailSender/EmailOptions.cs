namespace Infrastructure.WhoIsParking.Services.EmailSender;

internal class EmailOptions
{
    public string? Email { get; set; }
    public  string? Password { get; set; }
    public string? Host { get; set; }
    public int Port { get; set; }
}
