using System.ComponentModel.DataAnnotations.Schema;

namespace MarteliveryAPI_DotNet8_v01.Models
{
    [Table("payments")]
    public class Payment
    {
        [Column("payment_id", TypeName = "uuid")]
        public Guid PaymentId { get; set; }
        [Column("payment_method", TypeName = "varchar(250)")]
        public required string PaymentMethod { get; set; }
        [Column("payment_status", TypeName = "varchar(250)")]
        public required string PaymentStatus { get; set; }
        [Column("payment_amount")]
        public required float PaymentAmount { get; set; }
        [Column("payment_time")]
        public required DateTime PaymentTime { get; set; }
        [Column("quote_id", TypeName = "uuid")]
        public required Guid QuoteId { get; set; }
        public required Quote Quote { get; set; }
    }
}
