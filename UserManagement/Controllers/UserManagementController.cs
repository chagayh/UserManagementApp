using Microsoft.AspNetCore.Mvc;
using UserManagement.Interfaces;
using UserManagement.Models;

namespace UserManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserManagementController : ControllerBase
{
    private readonly ILogger<UserManagementController> _logger;
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;

    public UserManagementController(ILogger<UserManagementController> logger, IUserService userService,  IGroupService groupService)
    {
        _logger = logger;
        _groupService =  groupService;
        _userService = userService;
    }
    
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUserWithPagination([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        if (offset < 0 || limit < 1)
        {
            return BadRequest(new { message = "limit and offset must be >= 1" });
        }
        
        try
        {   
            var users = await _userService.GetUsers(offset, limit);
            return Ok(users);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error calling external distributor service.");
            return StatusCode(503, "Error communicating with flight distributor.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Something went wrong.");
        }
    }
    
    [HttpGet("groups")]
    public async Task<IActionResult> GetAllGroupsWithPagination([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        if (offset < 0 || limit < 1)
        {
            return BadRequest(new { message = "limit and offset must be >= 1" });
        }
        
        try
        {   
            var groups = await _groupService.GetGroups(offset, limit);
            return Ok(groups);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error calling external distributor service.");
            return StatusCode(503, "Error communicating with flight distributor.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Something went wrong.");
        }
    }
    
    [HttpDelete("{userId}/group")]
    public async Task<IActionResult> RemoveUserFromGroup(int userId)
    {
        try
        {
            await _groupService.RemoveUserFromGroupAsync(userId);
            return NoContent(); // 204
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error calling external distributor service.");
            return StatusCode(503, "Error communicating with flight distributor.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Something went wrong.");
        }
    }
}