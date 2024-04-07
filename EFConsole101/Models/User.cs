namespace EFConsole101.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public static User GenerateRandomUser()
    {
        Random random = new();
        int uniqueId = random.Next(1000, 9999);
        return new User
        {
            Username = $"User{uniqueId}",
            Email = $"user{uniqueId}@example.com",
            CreatedDate = DateTime.UtcNow
        };
    }
}
