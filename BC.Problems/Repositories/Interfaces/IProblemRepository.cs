﻿using BC.Messaging;
using BC.Problems.Boundary.Features;
using BC.Problems.Models;

namespace BC.Problems.Repositories.Interfaces;

public interface IProblemRepository
{
    Task<PagedList<Problem>> GetProblemsAsync(ProblemParameters problemParameters);
    Task<Problem> GetProblemAsync(Guid id);
    Task CreateProblemAsync(Problem problem);
    Task DeleteProblemAsync(Problem problem);
    Task UpdateProblemAsync(Problem problem);
    Task<PagedList<Problem>> GetUserProblemsAsync(Guid userId, ProblemParameters problemParameters);
    Task<PagedList<Problem>> GetMasterProblemsAsync(Guid masterId, ProblemParameters problemParameters);
    Task<PagedList<Problem>> GetNewProblemsAsync(ProblemParameters problemParameters);
    Task<Problem> GetProblemWithPartsAsync(Guid id);
    Task UpdateProblemsUserInfoAsync(UserUpdated userUpdated);
    Task DeleteProblemsUserInfoAsync(UserDeleted userDeleted);
}
