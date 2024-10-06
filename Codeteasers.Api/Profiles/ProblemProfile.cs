using AutoMapper;
using Domain.Entities;
using Presentation.Entities.Creation;
using Presentation.Entities.View;

namespace Presentation.Profiles;

public class ProblemProfile : Profile
{
    public ProblemProfile()
    {
        CreateMap<ProblemForCreation, Problem>();
        CreateMap<Problem, ProblemForView>();
    }
}
