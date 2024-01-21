namespace MarteliveryAPI.Models.DTOs.Admin
{
    public class AdminQuoteInfoDTO
    {
        public string QuoteId { get; set; }
        public float PricePerKm { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string ParcelId { get; set; }
    }
}
