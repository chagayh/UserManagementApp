using UserManagement.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;
using UserManagement.Types;

namespace UserManagement.DataAccess;

public class DataAccess : IDataAccess
{
    private readonly AppDbContext _context;

    public DataAccess(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }
    
    public async Task<IEnumerable<GroupDto>> GetGroupsAsync()
    {
        var query = _context.UserGroups
            .GroupBy(ug => ug.GroupId)
            .Select(g => new GroupDto
            {
                GroupId = g.Key,
                UserIds = g.Select(x => x.UserId).ToList()
            });

        var groups = await query
            .ToListAsync();

        return groups;
    }
    
    public async Task RemoveUserFromGroupAsync(int userId)
    {
        var user = await _context.Users
            .Include(u => u.UserGroup)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) 
            throw new Exception("User not found");

        if (user.UserGroup != null)
        {
            var group = user.UserGroup;

            _context.UserGroups.Remove(group); 
            await _context.SaveChangesAsync();

            // check if group is empty
            var remaining = await _context.UserGroups
                .CountAsync(ug => ug.GroupId == group.GroupId);
            
            // update group status 
            if (remaining == 0)
            {
                group.Status = GroupStatus.Empty;
                _context.UserGroups.Update(group);
                await _context.SaveChangesAsync();
            }
        }
    }
}