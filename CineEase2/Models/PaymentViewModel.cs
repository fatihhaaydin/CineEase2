namespace CineEase2.Models
{
    public class PaymentViewModel
    {
        public string Title { get; set; }
        public Ticket Ticket { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}
