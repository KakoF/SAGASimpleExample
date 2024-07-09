using MassTransit;
using Newsletter.Api.Messages;

namespace Newsletter.Api.Consumers;

public class OnboardingCompletedConsumer(ILogger<OnboardingCompletedConsumer> logger) : IConsumer<OnboardingCompleted>
{
    public Task Consume(ConsumeContext<OnboardingCompleted> context)
    {
        logger.LogInformation("Onboarding completed");

        return Task.CompletedTask;
    }
}
