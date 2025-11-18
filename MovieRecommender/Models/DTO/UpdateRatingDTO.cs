namespace MovieRecommender.Models.DTO
{
    public class UpdateRatingDTO
    {
        public int Score { get; set; }
        public string Review { get; set; } = string.Empty;
    }
}
