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
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsCustomer { get; set; }

        public List<Parcel>? Parcels { get; set; }
        public List<CarrierRating>? CarrierRatings { get; set; }
        public List<Quote>? Quotes { get; set; }
    }
}
