namespace MovieRecommender.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
