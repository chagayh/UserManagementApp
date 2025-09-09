using UserManagement.Models;

namespace UserManagement.Interfaces;

public interface IGroupService
{
    public Task<IEnumerable<GroupDto>?> GetGroups(int offset, int limit);

    public Task RemoveUserFromGroupAsync(int id);
}