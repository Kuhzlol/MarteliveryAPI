namespace MarteliveryAPI.Models.DTOs.Customer
{
    public class CustomerQuoteDTO
    {
        public string QuoteId { get; set; }
        public decimal PricePerKm { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string ParcelId { get; set; }
    }
}
