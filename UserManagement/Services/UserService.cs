using Microsoft.EntityFrameworkCore.Storage;
using UserManagement.Interfaces;
using UserManagement.Models;

namespace UserManagement.Services;

public class UserService : IUserService
{
    private readonly IDataAccess _dataAccess;
    
    public UserService(IDataAccess  dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    public async Task<IEnumerable<UserWithGroupDto>?> GetUsers(int offset, int limit)
    {
        var results = await _dataAccess.GetUsersAsync();

        var users = results
            .Skip(offset)
            .Take(limit)
            .Select(u => new UserWithGroupDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                GroupId = u.UserGroup?.GroupId
            });
        
        return users;
    }

}