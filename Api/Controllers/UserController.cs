using Application.Enums;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

        await _userService.Upload(file.OpenReadStream());
        
        return Ok("CSV file uploaded and processed successfully.");
    }

    [HttpGet]
    public IActionResult Get(SortingType sortingType, int length)
    {
        return Ok(_userService.GetUsers(sortingType, length));
    }
}