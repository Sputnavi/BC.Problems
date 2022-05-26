using BC.Messaging;
using BC.Problems.Boundary.Features;
using BC.Problems.Models;

namespace BC.Problems.Repositories.Interfaces
{
    public interface IBicycleRepository
    {
        Task<PagedList<Bicycle>> GetBicyclesAsync(BicycleParameters bicycleParameters);
        Task<Bicycle> GetBicycleAsync(Guid id);
        Task CreateBicycleAsync(Bicycle bicycle);
        Task DeleteBicycleAsync(Bicycle bicycle);
        Task UpdateBicycleAsync(Bicycle bicycle);
        Task UpdateBicyclesUserInfoAsync(UserUpdated userUpdated);
        Task DeleteBicyclesUserInfoAsync(UserDeleted userUpdated);
    }
}
