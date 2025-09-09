namespace UserManagement.Models;

public class UserWithGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int? GroupId { get; set; }
}