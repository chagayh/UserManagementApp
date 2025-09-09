namespace UserManagement.Models;

public class GroupDto
{
    public int GroupId { get; set; }
    public List<int> UserIds { get; set; } = new();
}