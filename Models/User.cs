using System.ComponentModel.DataAnnotations;

namespace Verification.Models
{
    public class User : BaseEntity
    {
        [Required] [StringLength(100)] public string FullName { get; set; }

        [Required] [Phone] public string Phone { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }
    }
}