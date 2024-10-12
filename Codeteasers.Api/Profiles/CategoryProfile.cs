using AutoMapper;
using Domain.Entities;
using Presentation.Entities.View;

namespace Presentation.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryForView>();
    }
}
