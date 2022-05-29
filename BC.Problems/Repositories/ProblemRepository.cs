using BC.Problems.Boundary.Features;
using BC.Problems.Models;
using BC.Problems.Repositories.Extensions;
using BC.Problems.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BC.Problems.Repositories
{
    public class ProblemRepository : RepositoryBase<Problem>, IProblemRepository
    {
        public ProblemRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public Task CreateProblemAsync(Problem problem) => CreateAsync(problem);

        public Task DeleteProblemAsync(Problem problem) => DeleteAsync(problem);

        public Task UpdateProblemAsync(Problem problem) => UpdateAsync(problem);

        public async Task<Problem> GetProblemAsync(Guid id) =>
            await FindByCondition(p => p.Id.Equals(id)).SingleOrDefaultAsync();

        public async Task<PagedList<Problem>> GetProblemsAsync(ProblemParameters problemParameters)
        {
            var problems = await FindAll()
                .Search(problemParameters.SearchTerm)
                .Sort(problemParameters.OrderBy)
                .ToListAsync();

            return PagedList<Problem>.ToPagedList(problems, problemParameters.PageNumber, problemParameters.PageSize);
        }
    }
}
