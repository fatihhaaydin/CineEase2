using Microsoft.AspNetCore.Identity;

namespace CineEase2.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Status { get; set; }
        public virtual List<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}
