namespace MovieRecommender.Models.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MoviesCount { get; set; }
    }
}
