using System.Diagnostics;
using System.Globalization;
using Application.Dto;
using Application.Enums;
using Persistence;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService
{
    private readonly TaskDbContext _dbContext;

    public UserService(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Upload(Stream stream)
    {
        using var csvStreamReader = new StreamReader(stream);
        using var csv = new CsvReader(csvStreamReader, CultureInfo.InvariantCulture);
        var users = csv.GetRecords<User>().ToList();

        foreach (var userDto in users)
        {
            var userEntity = await _dbContext.Users!.FirstOrDefaultAsync(u => u.Id == userDto.Id);

            if (userEntity != null)
            {
                userEntity.Username = userDto.Username;
                userEntity.Age = userDto.Age;
                userEntity.City = userDto.City;
                userEntity.PhoneNumber = userDto.PhoneNumber;
                userEntity.Email = userDto.Email;
            }
            else
            {
                var newUserEntity = new Persistence.Entities.User(
                    userDto.Id,
                    userDto.Username,
                    userDto.Age,
                    userDto.City,
                    userDto.PhoneNumber,
                    userDto.Email);

                await _dbContext.Users!.AddAsync(newUserEntity);
            }
            await _dbContext.SaveChangesAsync();
        }
    }

    public IEnumerable<User> GetUsers(SortingType sortingType, int length)
    {
        var usersQuery = _dbContext.Users!.ToList();
        var usersBySortingType = sortingType switch
        {
            SortingType.UsernameAscending => usersQuery.OrderBy(u => u.Username),
            SortingType.UsernameDescending => usersQuery.OrderByDescending(u => u.Username),
            _ => throw new ArgumentOutOfRangeException(nameof(sortingType), sortingType, null)
        };

        var usersByLength = usersBySortingType.Take(length);

        foreach (var user in usersByLength)
        {
            yield return new User(
                user.Username,
                user.Id,
                user.Age,
                user.City,
                user.PhoneNumber,
                user.Email);
        }
    }
}