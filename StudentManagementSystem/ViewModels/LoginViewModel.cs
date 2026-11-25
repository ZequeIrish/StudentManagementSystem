using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.ViewModels;

// What the login form sends to the server.
public class LoginViewModel
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50)]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
}
