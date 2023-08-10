namespace Application.Dto;

public record User(
    string Username, 
    Guid Id, 
    int Age, 
    string City, 
    string PhoneNumber, 
    string Email);