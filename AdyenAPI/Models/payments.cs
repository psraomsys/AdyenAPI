namespace AdyenAPI.Models
{
    public class payments
    {
        public Guid Id { get; set; }
        public string? merchantAccount { get; set; }
        public int amount { get; set; }
        public string? channel { get; set; }
        public long countryCode { get; set;}
        public string? shopperLocale { get; set; }
    }
}
