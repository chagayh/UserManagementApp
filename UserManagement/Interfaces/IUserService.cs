using UserManagement.Models;

namespace UserManagement.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<UserWithGroupDto>?> GetUsers(int offset, int limit);
}