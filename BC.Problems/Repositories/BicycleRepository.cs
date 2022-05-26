using BC.Messaging;
using BC.Problems.Boundary.Features;
using BC.Problems.Models;
using BC.Problems.Repositories.Extensions;
using BC.Problems.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BC.Problems.Repositories
{
    public class BicycleRepository : RepositoryBase<Bicycle>, IBicycleRepository
    {
        public BicycleRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public Task CreateBicycleAsync(Bicycle bicycle) => CreateAsync(bicycle);

        public Task DeleteBicycleAsync(Bicycle bicycle) => DeleteAsync(bicycle);

        public Task UpdateBicycleAsync(Bicycle bicycle) => UpdateAsync(bicycle);

        public async Task<Bicycle> GetBicycleAsync(Guid id) =>
            await FindByCondition(p => p.Id.Equals(id)).SingleOrDefaultAsync();

        public async Task<PagedList<Bicycle>> GetBicyclesAsync(BicycleParameters bicycleParameters)
        {
            var bicycles = await FindAll()
                .Search(bicycleParameters.SearchTerm)
                .Sort(bicycleParameters.OrderBy)
                .ToListAsync();

            return PagedList<Bicycle>.ToPagedList(bicycles, bicycleParameters.PageNumber, bicycleParameters.PageSize);
        }

        public async Task UpdateBicyclesUserInfoAsync(UserUpdated userUpdated)
        {
            var bicyclesToUpdate = FindByCondition(x => x.UserId == userUpdated.Id);

            foreach (var bicycle in bicyclesToUpdate)
            {
                bicycle.Email = userUpdated.Email;
            }

            await _repositoryContext.SaveChangesAsync();
        }

        public async Task DeleteBicyclesUserInfoAsync(UserDeleted userUpdated)
        {
            var bicyclesToUpdate = FindByCondition(x => x.UserId == userUpdated.Id);

            foreach (var bicycle in bicyclesToUpdate)
            {
                bicycle.UserId = null;
            }

            await _repositoryContext.SaveChangesAsync();
        }
    }
}
