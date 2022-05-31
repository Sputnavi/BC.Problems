using AutoMapper;
using BC.Problems.Boundary.Features;
using BC.Problems.Boundary.Request;
using BC.Problems.Boundary.Response;
using BC.Problems.Models;
using BC.Problems.Models.Exceptions;
using BC.Problems.Repositories.Interfaces;
using BC.Problems.Services.Interfaces;
using Newtonsoft.Json;

namespace BC.Problems.Services;

/// <summary>
/// Service to manage problems.
/// </summary>
public class ProblemService : IProblemService
{
    private readonly IMapper _mapper;
    private readonly IProblemRepository _problemRepository;
    private readonly ILogger<ProblemService> _logger;

    public ProblemService(IMapper mapper, IProblemRepository problemRepository, ILogger<ProblemService> logger)
    {
        _mapper = mapper;
        _problemRepository = problemRepository;
        _logger = logger;
    }

    public async Task<List<ProblemForReadModel>> GetProblemListAsync(ProblemParameters problemParameters, HttpResponse response = null)
    {
        var problems = await _problemRepository.GetProblemsAsync(problemParameters);

        if (response != null)
        {
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(problems.MetaData));
        }

        return _mapper.Map<List<ProblemForReadModel>>(problems);
    }
    
    public async Task<List<ProblemForReadModel>> GetUserProblemListAsync(Guid userId, ProblemParameters problemParameters, HttpResponse response = null)
    {
        var problems = await _problemRepository.GetUserProblemsAsync(userId, problemParameters); // ToDo: should we check user id ??

        if (response != null)
        {
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(problems.MetaData));
        }

        return _mapper.Map<List<ProblemForReadModel>>(problems);
    }

    public async Task<List<ProblemForReadModel>> GetMasterProblemListAsync(Guid masterId, ProblemParameters problemParameters, HttpResponse response = null)
    {
        var problems = await _problemRepository.GetMasterProblemsAsync(masterId, problemParameters); // ToDo: should we check master id ??

        if (response != null)
        {
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(problems.MetaData));
        }

        return _mapper.Map<List<ProblemForReadModel>>(problems);
    }

    public async Task<List<ProblemForReadModel>> GetNewProblemListAsync(ProblemParameters problemParameters, HttpResponse response = null)
    {
        var problems = await _problemRepository.GetNewProblemsAsync(problemParameters);

        if (response != null)
        {
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(problems.MetaData));
        }

        return _mapper.Map<List<ProblemForReadModel>>(problems);
    }

    public async Task<ProblemForReadModel> GetProblemAsync(Guid id)
    {
        var problemEntity = await _problemRepository.GetProblemAsync(id);
        CheckIfFound(id, problemEntity);
        return _mapper.Map<ProblemForReadModel>(problemEntity);
    }

    public async Task<Guid> CreateProblemAsync(ProblemForCreateModel model)
    {
        var problemEntity = _mapper.Map<Problem>(model);

        await _problemRepository.CreateProblemAsync(problemEntity);

        return problemEntity.Id;
    }

    public async Task DeleteProblemAsync(Guid id)
    {
        var problemEntity = await _problemRepository.GetProblemAsync(id);
        CheckIfFound(id, problemEntity);

        await _problemRepository.DeleteProblemAsync(problemEntity);
    }

    public async Task UpdateProblemAsync(Guid id, ProblemForUpdateModel model)
    {
        var problemEntity = await _problemRepository.GetProblemAsync(id);
        CheckIfFound(id, problemEntity);

        _mapper.Map(model, problemEntity);
        await _problemRepository.UpdateProblemAsync(problemEntity);
    }

    public async Task<ProblemForUpdateModel> GetProblemForUpdateModelAsync(Guid id)
    {
        var problemEntity = await GetProblemAsync(id);

        return _mapper.Map<ProblemForUpdateModel>(problemEntity);
    }

    private void CheckIfFound(Guid id, Problem problemEntity)
    {
        if (problemEntity is null)
        {
            _logger.LogInformation($"Problem with id: {id} doesn't exist in the database.");
            throw new EntityNotFoundException(nameof(Problem), id);
        }
    }
}
