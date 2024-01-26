namespace MarteliveryAPI.Models.DTOs.Admin
{
    public class AdminUpdateQuoteDTO
    {
        public decimal PricePerKm { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string ParcelId { get; set; }
    }
}
