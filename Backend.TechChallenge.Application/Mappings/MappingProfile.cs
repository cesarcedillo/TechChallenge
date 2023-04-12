using AutoMapper;
using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Models;
using System.IO;

namespace Backend.TechChallenge.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DtoInputUser, User>()
                .ForMember(user => user.Money, expression => expression.MapFrom(dtoInputUser => decimal.Parse(dtoInputUser.Money)));
        }
    }
}
