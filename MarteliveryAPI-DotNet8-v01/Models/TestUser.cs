using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("test_users")]
    public class TestUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        // Password must contain at least 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")]
        public required string Password { get; set; }

        [Column("date_of_birth")]
        [Required]
        public DateOnly DateOfBirth { get; set; }
    }
}
