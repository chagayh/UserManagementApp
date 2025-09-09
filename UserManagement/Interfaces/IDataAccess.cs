using UserManagement.Models;

namespace UserManagement.Interfaces;

public interface IDataAccess
{
    public Task<List<User>> GetUsersAsync();

    public Task<IEnumerable<GroupDto>> GetGroupsAsync();

    public Task RemoveUserFromGroupAsync(int userId);
}