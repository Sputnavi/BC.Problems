using BC.Problems.Boundary.Features;
using BC.Problems.Boundary.Request;
using BC.Problems.Boundary.Response;

namespace BC.Problems.Services.Interfaces
{
    public interface IBicycleService
    {
        Task<List<BicycleForReadModel>> GetBicycleListAsync(BicycleParameters bicycleParameters, HttpResponse response);
        Task<BicycleForReadModel> GetBicycleAsync(Guid id);
        Task<Guid> CreateBicycleAsync(BicycleForCreateOrUpdateModel model);
        Task UpdateBicycleAsync(Guid id, BicycleForCreateOrUpdateModel model);
        Task DeleteBicycleAsync(Guid id);
        Task<BicycleForCreateOrUpdateModel> GetBicycleForUpdateModelAsync(Guid id);
    }
}
