using MovieRecommender.Models.DTO;

namespace MovieRecommender.Services
{
    public interface IGenreService
    {
        IEnumerable<GenreDTO> GetAllGenres();
        GenreDTO? GetGenreById(int id);
        GenreDTO CreateGenre(CreateGenreDTO createGenreDto);
        bool DeleteGenre(int id);
    }
}
