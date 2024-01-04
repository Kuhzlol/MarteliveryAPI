using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public string CustomerId { get; set; } = Guid.NewGuid().ToString();

        [Column("first_name", TypeName = "varchar(250)")]
        [Required]
        public required string FirstName { get; set; }

        [Column("last_name", TypeName = "varchar(250)")]
        [Required]
        public required string LastName { get; set; }

        [Column("email", TypeName = "varchar(250)")]
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Column("is_email_confirmed")]
        public bool? IsEmailConfirmed { get; set; } = false;

        [Column("password", TypeName = "varchar(250)")]
        [Required]
        [DataType(DataType.Password)]
        [MinLength(10)]
        // Password must contain at least 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{10,}$", 
            ErrorMessage = "Invalid password, the password must contain at least 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character")]
        public required string Password { get; set; }

        [Column("phone_number", TypeName = "varchar(250)")]
        [Required]
        public required string PhoneNumber { get; set; }

        [Column("date_of_birth")]
        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Column("login_provider", TypeName = "varchar(250)")]
        public string? LoginProvider { get; set; }

        [Column("token", TypeName = "varchar(250)")]
        public string? Token { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public List<Parcel>? Parcels { get; set; }
        public List<CarrierRating>? CarrierRatings { get; set; }
    }
}
