# Orchestrated Saga Example

Orchestrated Saga example with:
- RabbitMQ
- PostgreSQL

## Running this project

Running should be easy enough since I added orchestration with `docker compose`.

Default database name is `Newsletter`.

## Eventos de fila:

- SubscribeToNewsletterConsumer

- SendWelcomeEmailConsumer

- SendFollowUpEmailConsumer

- OnboardingCompletedConsumer

## Eventos do SAGA

Todos os eventos são mapeados e correlacionados pela SubscriberId que é uma propriedade em todos o Events
Event(() => SubscriberCreated, e => e.CorrelateById(m => m.Message.SubscriberId));

- SubscriberCreated

- WelcomeEmailSent

- FollowUpEmailSent


https://learn.microsoft.com/pt-br/azure/architecture/reference-architectures/saga/saga