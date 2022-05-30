using BC.Problems.Boundary.Features;
using BC.Problems.Boundary.Request;
using BC.Problems.Boundary.Response;
using BC.Problems.Helpers.Extensions;
using BC.Problems.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BC.Problems.Controllers;

[Route("api/problems")]
[ApiController]
public class ProblemsController : ControllerBase
{
    private readonly IProblemService _problemService;
    private readonly ILogger<ProblemsController> _logger;

    public ProblemsController(ILogger<ProblemsController> logger, IProblemService problemService)
    {
        _logger = logger;
        _problemService = problemService;
    }

    /// <summary>
    /// Return a list of all Problems.
    /// </summary>
    /// <response code="200">List of problems returned successfully</response>
    /// <response code="401">You need to authorize first</response>
    /// <response code="403">Your role dosn't have enough rights</response>
    /// <response code="500">Internal Server Error</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProblemForReadModel[]))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpGet(Name = "GetProblemList")]
    [HttpHead(Name = "HeadProblemList")]
    public async Task<IActionResult> GetProblemList([FromQuery] ProblemParameters problemParameters)
    {
        var problems = await _problemService.GetProblemListAsync(problemParameters, Response);

        return Ok(problems);
    }

    /// <summary>
    /// Return Problem.
    /// </summary>
    /// <param name="id" example="D9F9841A-AACF-4BC4-924C-04C46E8274F1">The value that is used to find problem</param>
    /// <response code="200">Problem returned successfully</response> 
    /// <response code="401">You need to authorize first</response>
    /// <response code="403">Your role dosn't have enough rights</response>
    /// <response code="404">Problem with provided id cannot be found!</response>
    /// <response code="500">Internal Server Error</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProblemForReadModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpGet("{id}", Name = "GetProblem")]
    public async Task<IActionResult> GetProblem(Guid id)
    {
        var problemEntity = await _problemService.GetProblemAsync(id);

        return Ok(problemEntity);
    }

    /// <summary>
    /// Create new Problem.
    /// </summary>
    /// <param name="problem">The Problem object for creation</param>
    /// <response code="201">Problem created successfully</response> 
    /// <response code="400">Problem model is invalid</response>
    /// <response code="401">You need to authorize first</response>
    /// <response code="403">Your role dosn't have enough rights</response>
    /// <response code="500">Internal Server Error</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpPost(Name = "CreateProblem")]
    public async Task<IActionResult> CreateProblem([FromBody] ProblemForCreateModel problem)
    {
        this.ValidateObject();

        var problemId = await _problemService.CreateProblemAsync(problem);

        return CreatedAtRoute("GetProblem", new { id = problemId }, new AddedResponse(problemId));
    }

    /// <summary>
    /// Delete Problem.
    /// </summary>
    /// <param name="id" example="D9F9841A-AACF-4BC4-924C-04C46E8274F1">The value that is used to find Problem</param>
    /// <response code="204">Problem deleted successfully</response>
    /// <response code="401">You need to authorize first</response>
    /// <response code="403">Your role dosn't have enough rights</response>
    /// <response code="404">Problem with provided id cannot be found!</response>
    /// <response code="500">Internal Server Error</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpDelete("{id}", Name = "DeleteProblem")]
    public async Task<IActionResult> DeleteProblem(Guid id)
    {
        await _problemService.DeleteProblemAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Update Problem information.
    /// </summary>
    /// <param name="id" example="D9F9841A-AACF-4BC4-924C-04C46E8274F1">The value that is used to find Problem</param>
    /// <param name="problem">The Problem object which is used for update Problem with provided id</param>
    /// <response code="204">Problem updated successfully</response>
    /// <response code="400">Problem model is invalid</response>
    /// <response code="401">You need to authorize first</response>
    /// <response code="403">Your role dosn't have enough rights</response>
    /// <response code="404">Problem with provided id cannot be found!</response>
    /// <response code="500">Internal Server Error</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpPut("{id}", Name = "UpdateProblem")]
    public async Task<IActionResult> UpdateProblem(Guid id, [FromBody] ProblemForUpdateModel problem)
    {
        this.ValidateObject();

        await _problemService.UpdateProblemAsync(id, problem);

        return NoContent();
    }

    /// <summary>
    /// Partially update Problem information.
    /// </summary>
    /// <param name="id" example="D9F9841A-AACF-4BC4-924C-04C46E8274F1">The value that is used to find Problem</param>
    /// <param name="patchDoc">The document with an array of operations for Problem with provided id</param>
    /// <response code="204">Problem updated successfully</response>
    /// <response code="400">Problem model is invalid</response>
    /// <response code="401">You need to authorize first</response>
    /// <response code="403">Your role dosn't have enough rights</response>
    /// <response code="404">Problem with provided id cannot be found!</response>
    /// <response code="500">Internal Server Error</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseModel))]
    [HttpPatch("{id}", Name = "PartiallyUpdateProblem") ]
    public async Task<IActionResult> PartiallyUpdateProblem(Guid id,
        [FromBody] JsonPatchDocument<ProblemForUpdateModel> patchDoc)
    {
        if (patchDoc is null)
        {
            _logger.LogError("patchDoc object sent from client is null.");
            return BadRequest("Sent patch document is empty.");
        }

        var problemToPatch = await _problemService.GetProblemForUpdateModelAsync(id);

        patchDoc.ApplyTo(problemToPatch, ModelState);

        TryValidateModel(problemToPatch);
        this.ValidateObject();

        await _problemService.UpdateProblemAsync(id, problemToPatch);

        return NoContent();
    }
}
