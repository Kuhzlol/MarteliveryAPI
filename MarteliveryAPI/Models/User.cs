using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI.Models
{
    [Table("users")]
    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        [Required]
        public required string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        [Required]
        public required string LastName { get; set; }

        [PersonalData]
        [Required]
        public DateOnly DateOfBirth { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
