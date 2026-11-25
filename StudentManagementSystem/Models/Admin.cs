namespace StudentManagementSystem.Models;

// An admin account that can log in to the system.
public class Admin
{
    public int AdminId { get; set; }

    public string Username { get; set; } = null!;

    // Never the real password: a one-way scrambled version made by PasswordHasher.
    public string PasswordHash { get; set; } = null!;
}
