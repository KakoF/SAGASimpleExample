using MassTransit;
using Newsletter.Api.Messages;

namespace Newsletter.Api.Sagas;

public class NewsletterOnboardingSaga : MassTransitStateMachine<NewsletterOnboardingSagaData>
{
	public State Welcoming { get; set; }
	public State FollowingUp { get; set; }
	public State Onboarding { get; set; }

	public Event<SubscriberCreated> SubscriberCreated { get; set; }
	public Event<WelcomeEmailSent> WelcomeEmailSent { get; set; }
	public Event<FollowUpEmailSent> FollowUpEmailSent { get; set; }

	public NewsletterOnboardingSaga()
	{
		InstanceState(x => x.CurrentState);

		Event(() => SubscriberCreated, e => e.CorrelateById(m => m.Message.SubscriberId));
		Event(() => WelcomeEmailSent, e => e.CorrelateById(m => m.Message.SubscriberId));
		Event(() => FollowUpEmailSent, e => e.CorrelateById(m => m.Message.SubscriberId));

		Initially(
			When(SubscriberCreated)
				.Then(context =>
				{
					context.Saga.SubscriberId = context.Message.SubscriberId;
					context.Saga.Email = context.Message.Email;
				})
				.TransitionTo(Welcoming)
				.Publish(context =>
				{
					return new SendWelcomeEmail(context.Message.SubscriberId, context.Message.Email);
				}));

		During(Welcoming,
			When(WelcomeEmailSent)
				.Then(context =>
				{
					context.Saga.WelcomeEmailSent = true;
				})
				.TransitionTo(FollowingUp)
				.Publish(context => { 
					return new SendFollowUpEmail(context.Message.SubscriberId, context.Message.Email); 
				}));

		During(FollowingUp,
			When(FollowUpEmailSent)
				.Then(context =>
				{
					context.Saga.FollowUpEmailSent = true;
					context.Saga.OnboardingCompleted = true;
				})
				.TransitionTo(Onboarding)
				.Publish(context => {
					return new OnboardingCompleted
					{
						SubscriberId = context.Message.SubscriberId,
						Email = context.Message.Email
					};
				})
				.Finalize());
	}
}
