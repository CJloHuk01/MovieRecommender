using AutoMapper;
using MovieRecommender.Models;
using MovieRecommender.Models.DTO;
using MovieRecommender.Repositories;

namespace MovieRecommender.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, IUserRepository userRepository,
                           IMovieRepository movieRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public RatingDTO AddRating(CreateRatingDTO createRatingDto)
        {
            var user = _userRepository.GetById(createRatingDto.UserId);
            var movie = _movieRepository.GetById(createRatingDto.MovieId);

            if (user == null || movie == null)
                throw new ArgumentException("User or Movie not found");

            if (HasUserRatedMovie(createRatingDto.UserId, createRatingDto.MovieId))
            {
                throw new InvalidOperationException("User has already rated this movie. Use update instead.");
            }

            if (createRatingDto.Score < 1 || createRatingDto.Score > 10)
            {
                throw new ArgumentException("Score must be between 1 and 10");
            }

            var rating = _mapper.Map<Rating>(createRatingDto);
            rating.RatedAt = DateTime.UtcNow;

            var createdRating = _ratingRepository.Create(rating);
            return _mapper.Map<RatingDTO>(createdRating);
        }
        public RatingDTO UpdateRating(int ratingId, UpdateRatingDTO updateRatingDto)
        {
            var existingRating = _ratingRepository.GetById(ratingId);
            if (existingRating == null)
                throw new ArgumentException("Rating not found");

            if (updateRatingDto.Score < 1 || updateRatingDto.Score > 10)
            {
                throw new ArgumentException("Score must be between 1 and 10");
            }

            existingRating.Score = updateRatingDto.Score;
            existingRating.Review = updateRatingDto.Review;
            existingRating.RatedAt = DateTime.UtcNow; 

            var updatedRating = _ratingRepository.Update(existingRating);
            return _mapper.Map<RatingDTO>(updatedRating);
        }

        public IEnumerable<RatingDTO> GetUserRatings(int userId)
        {
            var ratings = _ratingRepository.GetRatingsByUser(userId);
            return _mapper.Map<IEnumerable<RatingDTO>>(ratings);
        }

        public IEnumerable<RatingDTO> GetMovieRatings(int movieId)
        {
            var ratings = _ratingRepository.GetRatingsByMovie(movieId);
            return _mapper.Map<IEnumerable<RatingDTO>>(ratings);
        }

        public bool HasUserRatedMovie(int userId, int movieId)
        {
            return _ratingRepository.GetRatingsByUser(userId)
                .Any(r => r.MovieId == movieId);
        }
    }
}
