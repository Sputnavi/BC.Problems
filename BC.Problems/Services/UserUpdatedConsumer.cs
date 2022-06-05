using BC.Messaging;
using BC.Problems.Repositories.Interfaces;
using MassTransit;

namespace BC.Problems.Services;
public class UserUpdatedConsumer : IConsumer<UserUpdated>
{
    private readonly ILogger<UserUpdatedConsumer> _logger;
    private readonly IProblemRepository _problemRepository;

    public UserUpdatedConsumer(ILogger<UserUpdatedConsumer> logger, IProblemRepository problemRepository)
    {
        _logger = logger;
        _problemRepository = problemRepository;
    }

    public async Task Consume(ConsumeContext<UserUpdated> context)
    {
        var message = context.Message;
        _logger.LogInformation($"Got UserUpdated event: {message}");

        await _problemRepository.UpdateProblemsUserInfoAsync(message);
    }
}
