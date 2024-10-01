using AutoMapper;
using Domain.Entities;
using Web.Api.Entities.Creation;
using Web.Api.Entities.View;

namespace Web.Api.Profiles;

public class ProblemProfile : Profile
{
    public ProblemProfile() 
    {
        CreateMap<ProblemForCreation, Problem>();
        CreateMap<Problem, ProblemForView>();
    }
}
