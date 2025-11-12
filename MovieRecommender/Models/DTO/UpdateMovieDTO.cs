namespace MovieRecommender.Models.DTO
{
    public class UpdateMovieDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; } = string.Empty;
        public int GenreId { get; set; }
    }
}
