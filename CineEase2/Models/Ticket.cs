namespace CineEase2.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public float price { get; set; }
        public string discount { get; set; }
        public float netprice { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
