namespace Persistence.Entities;

public class User
{
    public User(Guid id, string username, int age, string city, string phoneNumber, string email)
    {
        Id = id;
        Username = username;
        Age = age;
        City = city;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public Guid Id { get; set; }
    public string Username { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}