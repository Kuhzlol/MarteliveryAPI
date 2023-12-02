namespace MarteliveryAPI_DotNet8_v01.Entities
{
    public class Customer
    {
        public required int Id { get; set; }
        public required string firstname { get; set; }
        public required string lastname { get; set; }
        public required string email { get; set; }
        public required bool email_confirmed { get; set; }
        public required string password_hash { get; set; }
        public string? mobile { get; set; }
        public DateOnly date_of_birth { get; set; }
        public string? login_provider { get; set; }
        public string? provider_key { get; set; }
    }
}
