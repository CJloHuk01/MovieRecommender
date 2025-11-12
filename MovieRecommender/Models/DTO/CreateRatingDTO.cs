namespace MovieRecommender.Models.DTO
{
    public class CreateRatingDTO
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Score { get; set; }
        public string Review { get; set; } = string.Empty;
    }
}
