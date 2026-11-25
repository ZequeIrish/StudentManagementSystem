using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.ViewModels;

// What the "Change Password" form sends to the server.
public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Current password is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "Current Password")]
    public string CurrentPassword { get; set; } = "";

    [Required(ErrorMessage = "New password is required.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "New password must be at least 8 characters.")]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; } = "";

    [Required(ErrorMessage = "Please type the new password again.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm New Password")]
    [Compare(nameof(NewPassword), ErrorMessage = "The two new passwords do not match.")]
    public string ConfirmNewPassword { get; set; } = "";
}
