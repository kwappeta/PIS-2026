using Domain.Events;

namespace Domain.Events;

public record BookCompletedEvent(Guid BookId, DateTime OccurredOn) : IDomainEvent;