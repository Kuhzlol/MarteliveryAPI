using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI.Entities
{
    [Table("carriers")]
    public class Carrier
    {
        [Key]
        [Column("carrier_id")]
        public string CarrierId { get; set; } = Guid.NewGuid().ToString();

        [Column("first_name", TypeName = "varchar(250)")]
        public required string FirstName { get; set; }

        [Column("last_name", TypeName = "varchar(250)")]
        public required string LastName { get; set; }

        [Column("email", TypeName = "varchar(250)")]
        public required string Email { get; set; }

        [Column("is_email_confirmed")]
        public bool? IsEmailConfirmed { get; set; }

        [Column("hashed_password", TypeName = "varchar(250)")]
        public string? HashedPassword { get; set; }

        [Column("phone_number", TypeName = "varchar(250)")]
        public required string PhoneNumber { get; set; }

        [Column("date_of_birth")]
        public required DateOnly DateOfBirth { get; set; }

        [Column("login_provider", TypeName = "varchar(250)")]
        public string? LoginProvider { get; set; }

        [Column("token", TypeName = "varchar(250)")]
        public string? Token { get; set; }

        public List<Quote>? Quotes { get; set; }
    }
}
