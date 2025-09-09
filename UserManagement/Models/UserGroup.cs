using UserManagement.Types;

namespace UserManagement.Models;

public class UserGroup
{
    public int UserId { get; set; }   
    public int GroupId { get; set; }
    public User? User { get; set; }
    public GroupStatus Status { get; set; }
}