using System;
using System.ComponentModel.DataAnnotations;

namespace Verification.Dtos
{
    public class UserCreateDto
    {
        [Required] public string FullName { get; set; }
        [Required] public string Phone { get; set; }
        [Required] public string Email { get; set; }
    }
}