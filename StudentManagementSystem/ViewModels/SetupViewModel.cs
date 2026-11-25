using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.ViewModels;

// What the "create the first admin" form sends to the server.
public class SetupViewModel
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be 3 to 50 characters.")]
    [RegularExpression(@"^[A-Za-z0-9._-]+$", ErrorMessage = "Username can only contain letters, numbers, dots (.), dashes (-) and underscores (_).")]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Please type the password again.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare(nameof(Password), ErrorMessage = "The two passwords do not match.")]
    public string ConfirmPassword { get; set; } = "";
}
