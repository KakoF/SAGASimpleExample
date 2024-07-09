using MassTransit;
using Newsletter.Api.Services;
using Newsletter.Api.Messages;

namespace Newsletter.Api.Consumers;

public class SendFollowUpEmailConsumer(IEmailService emailService) : IConsumer<SendFollowUpEmail>
{
    public async Task Consume(ConsumeContext<SendFollowUpEmail> context)
    {
        await emailService.SendFollowUpEmailAsync(context.Message.Email);

        await context.Publish(new FollowUpEmailSent
        {
            SubscriberId = context.Message.SubscriberId,
            Email = context.Message.Email
        });
    }
}
