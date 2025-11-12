

using MovieRecommender.Models.DTO;

namespace MovieRecommender.Services
{
    public interface IMovieService
    {
        IEnumerable<MovieDTO> GetAllMovies();
        MovieDTO? GetMovieById(int id);
        IEnumerable<MovieDTO> GetMoviesByGenre(int genreId);
        MovieDTO CreateMovie(CreateMovieDTO createMovieDto);
        MovieDTO? UpdateMovie(int id, UpdateMovieDTO updateMovieDto);
        bool DeleteMovie(int id);
        IEnumerable<MovieDTO> GetTopRatedMovies(int count = 10);
    }
}
