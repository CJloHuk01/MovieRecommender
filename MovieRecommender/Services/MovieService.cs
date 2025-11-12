using AutoMapper;
using MovieRecommender.Models;
using MovieRecommender.Models.DTO;
using MovieRecommender.Repositories;

namespace MovieRecommender.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public IEnumerable<MovieDTO> GetAllMovies()
        {
            var movies = _movieRepository.GetAll();
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public MovieDTO? GetMovieById(int id)
        {
            var movie = _movieRepository.GetById(id);
            return _mapper.Map<MovieDTO>(movie);
        }

        public IEnumerable<MovieDTO> GetMoviesByGenre(int genreId)
        {
            var movies = _movieRepository.GetMoviesByGenre(genreId);
            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }

        public MovieDTO CreateMovie(CreateMovieDTO createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);

            var createdMovie = _movieRepository.Create(movie);
            return _mapper.Map<MovieDTO>(createdMovie);
        }

        public MovieDTO? UpdateMovie(int id, UpdateMovieDTO updateMovieDto)
        {
            var existingMovie = _movieRepository.GetById(id);
            if (existingMovie == null) return null;

            _mapper.Map(updateMovieDto, existingMovie);
            var updatedMovie = _movieRepository.Update(existingMovie);
            return _mapper.Map<MovieDTO>(updatedMovie);
        }

        public bool DeleteMovie(int id)
        {
            return _movieRepository.Delete(id);
        }

        public IEnumerable<MovieDTO> GetTopRatedMovies(int count = 10)
        {
            var movies = _movieRepository.GetAll();
            var movieDtos = _mapper.Map<IEnumerable<MovieDTO>>(movies);
            return movieDtos.OrderByDescending(m => m.AverageRating).Take(count);
        }
    }
}
