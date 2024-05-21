namespace CineEase2.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string Genre { get; set; }
        public float IMDB_Score { get; set; }
        public int Duration { get; set; }
        public int ReleaseYear { get; set; }
        public string Rating { get; set; }
        public virtual List<Actor> Actors { get; set; } = new List<Actor>();
    }
}
