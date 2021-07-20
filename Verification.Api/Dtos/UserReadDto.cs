using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Verification.Api.Dtos
{
    public class UserReadDto
    {
        [Required] public int Id { get; set; }

        [Required] [StringLength(100)] public string FullName { get; set; }

        [Required] [Phone] public string Phone { get; set; }

        [EmailAddress] [Required] public string Email { get; set; }
    }
}