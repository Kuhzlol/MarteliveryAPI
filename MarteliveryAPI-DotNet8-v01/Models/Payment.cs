using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("payments")]
    public class Payment
    {
        [Key]
        [Column("payment_id")]
        public string PaymentId { get; set; } = Guid.NewGuid().ToString();

        [Column("payment_method", TypeName = "varchar(250)")]
        public required string PaymentMethod { get; set; }

        [Column("payment_status", TypeName = "varchar(250)")]
        public required string PaymentStatus { get; set; } = "Pending";

        [Column("payment_amount")]
        public required float PaymentAmount { get; set; }

        [Column("payment_time")]
        public required DateTime PaymentTime { get; set; } = DateTime.Now;

        [Column("quote_id")]
        public required string QuoteId { get; set; }
        public Quote? Quote { get; set; }
    }
}
