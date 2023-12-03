using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("customers")]
    public class Customer
    {
        [Column("customer_id")]
        public required int CustomerId { get; set; }
        [Column("first_name", TypeName = "varchar(250)")]
        public required string FirstName { get; set; }
        [Column("last_name", TypeName = "varchar(250)")]
        public required string LastName { get; set; }
        [Column("email", TypeName = "varchar(250)")]
        public required string Email { get; set; }
        [Column("is_email_confirmed")]
        public required bool IsEmailConfirmed { get; set; }
        [Column("hashed_password", TypeName = "varchar(250)")]
        public required string HashedPassword { get; set; }
        [Column("phone_number", TypeName = "varchar(250)")]
        public required string PhoneNumber { get; set; }
        [Column("date_of_birth")]
        public required DateOnly DateOfBirth { get; set; }
        [Column("login_provider", TypeName = "varchar(250)")]
        public string? LoginProvider { get; set; }
        [Column("token", TypeName = "varchar(250)")]
        public string? Token { get; set; }
    }
}
