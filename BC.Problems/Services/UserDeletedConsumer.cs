using BC.Messaging;
using BC.Problems.Repositories.Interfaces;
using MassTransit;

namespace BC.Problems.Services;
public class UserDeletedConsumer : IConsumer<UserDeleted>
{
    private readonly ILogger<UserDeletedConsumer> _logger;
    private readonly IProblemRepository _problemRepository;

    public UserDeletedConsumer(ILogger<UserDeletedConsumer> logger, IProblemRepository problemRepository)
    {
        _logger = logger;
        _problemRepository = problemRepository;
    }

    public async Task Consume(ConsumeContext<UserDeleted> context)
    {
        var message = context.Message;
        _logger.LogInformation($"Got UserDeleted event: {message}");

        await _problemRepository.DeleteProblemsUserInfoAsync(message);
    }
}