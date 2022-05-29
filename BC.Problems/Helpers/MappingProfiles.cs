using AutoMapper;
using BC.Problems.Boundary.Request;
using BC.Problems.Boundary.Response;
using BC.Problems.Models;

namespace BC.Problems.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Problem, ProblemForReadModel>();
        CreateMap<ProblemForCreateModel, Problem>()
            .ForMember(destination => destination.DateCreated, x => x.MapFrom(src => DateTime.UtcNow));
        
        CreateMap<ProblemForUpdateModel, Problem>().ReverseMap();
        CreateMap<ProblemForReadModel, ProblemForUpdateModel>();
    }
}