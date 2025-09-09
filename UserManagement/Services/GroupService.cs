using UserManagement.Interfaces;
using UserManagement.Models;

namespace UserManagement.Services;

public class GroupService : IGroupService
{
    private readonly IDataAccess _dataAccess;
    
    public GroupService(IDataAccess  dataAccess)
    {
        _dataAccess = dataAccess;
    }
    
    public async Task<IEnumerable<GroupDto>?> GetGroups(int offset, int limit)
    {
        var results = await _dataAccess.GetGroupsAsync();

        return results;
    }

    public async Task RemoveUserFromGroupAsync(int id)
    {
        await _dataAccess.RemoveUserFromGroupAsync(id);
    }
    
}