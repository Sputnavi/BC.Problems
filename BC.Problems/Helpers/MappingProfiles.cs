using AutoMapper;
using BC.Problems.Boundary.Request;
using BC.Problems.Boundary.Response;
using BC.Problems.Models;

namespace BC.Problems.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Bicycle, BicycleForReadModel>();
        CreateMap<BicycleForCreateOrUpdateModel, Bicycle>().ReverseMap();
        CreateMap<BicycleForReadModel, BicycleForCreateOrUpdateModel>();

        // ToDo: make here default start date.
    }
}