namespace MovieRecommender.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Score { get; set; }
        public string Review { get; set; } = string.Empty;
        public DateTime RatedAt { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
