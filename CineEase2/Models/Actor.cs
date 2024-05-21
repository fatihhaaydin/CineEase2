namespace CineEase2.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string ActorName { get; set; }
        public string ActorSurname { get; set; }
        public int ActorAge { get; set; }
        public string Nationality { get; set; }
        public int Follower { get; set; }
        public int MovieId { get; set; }
        public virtual Movie? Movie { get; set; }
    }
}
