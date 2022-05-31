using AutoMapper;
using BC.Problems.Boundary.Common;
using BC.Problems.Boundary.Request;
using BC.Problems.Boundary.Response;
using BC.Problems.Models;

namespace BC.Problems.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Problem, ProblemForReadModel>()
            .ForMember(dest => dest.Parts, x => x.MapFrom(src => src.PartModelProblems))
            .ForMember(dest => dest.Bicycle, x => x.MapFrom(src => new ProblemBicycleModel
            {
                Id = src.BicycleId,
                Model = src.BicycleModel,
                SerialNumber = src.BicycleSerialNumber
            }));
        CreateMap<ProblemForCreateModel, Problem>()
            .ForMember(destination => destination.DateCreated, x => x.MapFrom(src => DateTime.UtcNow))
            .ForMember(destination => destination.Stage, x => x.MapFrom(src => ProblemStage.New))
            .ForMember(dest => dest.PartModelProblems, x => x.MapFrom(src => src.Parts));

        CreateMap<ProblemAddressModel, Address>().ReverseMap();

        CreateMap<ProblemForUpdateModel, Problem>()
            .ForMember(destination => destination.Stage, x => x.MapFrom(src => Enum.Parse<ProblemStage>(src.Stage)));
        CreateMap<ProblemForReadModel, ProblemForUpdateModel>();

        CreateMap<ProblemPartModel, PartModelProblem>();
        CreateMap<PartModelProblem, ProblemPartModel>();
    }
}