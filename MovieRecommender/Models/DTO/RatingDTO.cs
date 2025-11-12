namespace MovieRecommender.Models.DTO
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Review { get; set; } = string.Empty;
        public DateTime RatedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string MovieTitle { get; set; } = string.Empty;
    }
}
