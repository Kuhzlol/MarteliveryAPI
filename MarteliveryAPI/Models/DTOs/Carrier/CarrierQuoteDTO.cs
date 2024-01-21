namespace MarteliveryAPI.Models.DTOs.Carrier
{
    public class CarrierQuoteDTO
    {
        public float PricePerKm { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string ParcelId { get; set; }
    }
}
