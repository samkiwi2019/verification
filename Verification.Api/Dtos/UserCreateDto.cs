using System;
using System.ComponentModel.DataAnnotations;

namespace Verification.Api.Dtos
{
    public class UserCreateDto
    {
        [Required] [StringLength(100)] public string FullName { get; set; }

        [Required] [Phone] public string Phone { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required] public string VCode { get; set; } // for verifying email. will not be written into the database;
    }
}