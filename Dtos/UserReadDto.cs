using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Verification.Dtos
{
    public class UserReadDto
    {
        [Required] public int Id { get; set; }
        [Required] public string FullName { get; set; }
        [Required] public string Phone { get; set; }
        [Required] public string Email { get; set; }
    }
}