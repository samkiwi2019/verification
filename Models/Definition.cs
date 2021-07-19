using System.ComponentModel.DataAnnotations;

namespace Verification.Models
{
    public class User: BaseEntity
    {
        [Required] public string FullName { get; set; }
        [Required] public string Phone { get; set; }
        [Required] public string Email { get; set; }
    }
}