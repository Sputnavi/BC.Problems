using BC.Messaging;
using BC.Problems.Boundary.Features;
using BC.Problems.Models;
using BC.Problems.Repositories.Extensions;
using BC.Problems.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BC.Problems.Repositories;

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
    public async Task<Problem> GetProblemWithPartsAsync(Guid id) =>
        await FindByCondition(p => p.Id.Equals(id))
        .Include(p => p.PartModelProblems)
        .SingleOrDefaultAsync();

    public async Task<PagedList<Problem>> GetProblemsAsync(ProblemParameters problemParameters)
    {
        var problems = await FindAll()
            .Search(problemParameters.SearchTerm)
            .Sort(problemParameters.OrderBy)
            .ToListAsync();

        return PagedList<Problem>.ToPagedList(problems, problemParameters.PageNumber, problemParameters.PageSize);
    }
    
    public async Task<PagedList<Problem>> GetUserProblemsAsync(Guid userId, ProblemParameters problemParameters)
    {
        var problems = await FindByCondition(p => p.UserId == userId)
            .Search(problemParameters.SearchTerm)
            .Sort(problemParameters.OrderBy)
            .Include(p => p.PartModelProblems)
            .ToListAsync();

        return PagedList<Problem>.ToPagedList(problems, problemParameters.PageNumber, problemParameters.PageSize);
    }

    public async Task<PagedList<Problem>> GetMasterProblemsAsync(Guid masterId, ProblemParameters problemParameters)
    {
        var problems = await FindByCondition(p => p.MasterId == masterId)
            .Search(problemParameters.SearchTerm)
            .Sort(problemParameters.OrderBy)
            .Include(p => p.PartModelProblems)
            .ToListAsync();

        return PagedList<Problem>.ToPagedList(problems, problemParameters.PageNumber, problemParameters.PageSize);
    }

    public async Task<PagedList<Problem>> GetNewProblemsAsync(ProblemParameters problemParameters)
    {
        var problems = await FindByCondition(p => p.Stage == ProblemStage.New)
            .Search(problemParameters.SearchTerm)
            .Sort(problemParameters.OrderBy)
            .Include(p => p.PartModelProblems)
            .ToListAsync();

        return PagedList<Problem>.ToPagedList(problems, problemParameters.PageNumber, problemParameters.PageSize);
    }

    public async Task UpdateProblemsUserInfoAsync(UserUpdated userUpdated)
    {
        var problemsToUpdate = FindByCondition(x => x.UserId == userUpdated.Id);

        foreach (var problem in problemsToUpdate)
        {
            problem.UserEmail = userUpdated.Email;
        }

        await _repositoryContext.SaveChangesAsync();
    }

    public async Task DeleteProblemsUserInfoAsync(UserDeleted userDeleted)
    {
        var problemsToUpdate = FindByCondition(x => x.UserId == userDeleted.Id);

        foreach (var problem in problemsToUpdate)
        {
            problem.UserId = null;
        }

        await _repositoryContext.SaveChangesAsync();
    }
}
