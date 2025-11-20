
using AutoMapper;
using MovieRecommender.Models;
using MovieRecommender.Models.DTO;

namespace MovieRecommender.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                    src.Ratings.Any() ? src.Ratings.Average(r => r.Score) : 0));
            CreateMap<CreateMovieDTO, Movie>();
            CreateMap<UpdateMovieDTO, Movie>();

            CreateMap<Genre, GenreDTO>()
                .ForMember(dest => dest.MoviesCount, opt => opt.MapFrom(src => src.Movies.Count));
            CreateMap<CreateGenreDTO, Genre>();

            CreateMap<Rating, RatingDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title));
            CreateMap<CreateRatingDTO, Rating>();
            CreateMap<UpdateRatingDTO, Rating>()
                .ForMember(dest => dest.RatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            
        }
    }
}

