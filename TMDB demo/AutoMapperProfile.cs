using AutoMapper;
using Repository.Entities;
using System.Text.RegularExpressions;
using TMDB_demo.Models.TMDB_models;

namespace TMDB_demo
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<MovieWithCredits, Movie>()               
                .ForMember(dest => dest.TmdbVoteAverage, opt => opt.MapFrom(src => src.VoteAverage))
                .ForMember(dest => dest.TmdbVoteCount, opt => opt.MapFrom(src => src.VoteCount))
                .ForMember(dest => dest.TmdbUrl, opt => opt.MapFrom(src => $"movie/{src.Id }"))                
                .ForMember(dest => dest.Directors, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore());

            CreateMap<TmdbDirector, Director>()                
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.BirthDay));

            CreateMap<TmdbGenre, Genre>();               
        }

        
    }
}
