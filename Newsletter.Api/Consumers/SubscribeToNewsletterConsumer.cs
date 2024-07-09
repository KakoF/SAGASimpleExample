using MassTransit;
using Newsletter.Api.Database;
using Newsletter.Api.Messages;

namespace Newsletter.Api.Consumers;

public class SubscribeToNewsletterConsumer(AppDbContext dbContext) : IConsumer<SubscribeToNewsletter>
{
    public async Task Consume(ConsumeContext<SubscribeToNewsletter> context)
    {
        var subscriber = dbContext.Subscribers.Add(new Subscriber
        {
            Id = Guid.NewGuid(),
            Email = context.Message.Email,
            SubscribedOnUtc = DateTime.UtcNow
        });

        await dbContext.SaveChangesAsync();

        await context.Publish(new SubscriberCreated
        {
            SubscriberId = subscriber.Entity.Id,
            Email = context.Message.Email
        });
    }
}
