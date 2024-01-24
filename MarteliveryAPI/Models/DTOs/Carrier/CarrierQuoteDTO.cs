namespace MarteliveryAPI.Models.DTOs.Carrier
{
    public class CarrierQuoteDTO
    {
        public decimal PricePerKm { get; set; }
        public decimal TotalPrice { get; set; }
        public string ParcelId { get; set; }
    }
}
