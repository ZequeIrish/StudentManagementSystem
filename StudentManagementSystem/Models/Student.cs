using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models;

public partial class Student
{
    // Letters (including accented letters like ñ and é), spaces, apostrophes, hyphens and periods.
    // The first character must be a letter.
    private const string NamePattern =
        @"^[A-Za-z\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u024F][A-Za-z\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u024F\s'.\-]*$";

    private const string NameRuleMessage =
        "can only contain letters, spaces, apostrophes ('), hyphens (-) and periods (.).";

    public int StudentId { get; set; }

    [Required(ErrorMessage = "First Name is required.")]
    [StringLength(50)]
    [RegularExpression(NamePattern, ErrorMessage = "First Name " + NameRuleMessage)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    [RegularExpression(NamePattern, ErrorMessage = "Middle Name " + NameRuleMessage)]
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Last Name is required.")]
    [StringLength(50)]
    [RegularExpression(NamePattern, ErrorMessage = "Last Name " + NameRuleMessage)]
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