using AutoMapper;
using MovieRecommender.Models;
using MovieRecommender.Models.DTO;
using MovieRecommender.Repositories;

namespace MovieRecommender.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public IEnumerable<GenreDTO> GetAllGenres()
        {
            var genres = _genreRepository.GetAll();
            return _mapper.Map<IEnumerable<GenreDTO>>(genres);
        }

        public GenreDTO? GetGenreById(int id)
        {
            var genre = _genreRepository.GetById(id);
            return _mapper.Map<GenreDTO>(genre);
        }

        public GenreDTO CreateGenre(CreateGenreDTO createGenreDto)
        {
            var genre = _mapper.Map<Genre>(createGenreDto);
            var createdGenre = _genreRepository.Create(genre);
            return _mapper.Map<GenreDTO>(createdGenre);
        }

        public bool DeleteGenre(int id)
        {
            return _genreRepository.Delete(id);
        }
    }
}
