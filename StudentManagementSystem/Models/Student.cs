using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models;

public partial class Student
{
    public int StudentId { get; set; }

    [Required(ErrorMessage = "First Name is required.")]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First Name can only contain letters.")]
    public string? FirstName { get; set; }

    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Middle Name can only contain letters.")]
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(50)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last Name can only contain letters.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Age is required.")]
    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    public int? Age { get; set; }

    [Required(ErrorMessage = "Course is required.")]
    [StringLength(50)]
    public string? Course { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(50)]
    public string? Address { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}