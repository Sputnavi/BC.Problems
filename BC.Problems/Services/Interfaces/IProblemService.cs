using BC.Problems.Boundary.Features;
using BC.Problems.Boundary.Request;
using BC.Problems.Boundary.Response;

namespace BC.Problems.Services.Interfaces;

public interface IProblemService
{
    Task<List<ProblemForReadModel>> GetProblemListAsync(ProblemParameters problemParameters, HttpResponse response);
    Task<ProblemForReadModel> GetProblemAsync(Guid id);
    Task<Guid> CreateProblemAsync(ProblemForCreateModel model);
    Task UpdateProblemAsync(Guid id, ProblemForCreateModel model);
    Task DeleteProblemAsync(Guid id);
    Task<ProblemForUpdateModel> GetProblemForUpdateModelAsync(Guid id);
}
