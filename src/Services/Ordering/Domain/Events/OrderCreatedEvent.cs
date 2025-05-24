using Domain.Abstractions;
using Domain.Models;

namespace Domain.Events;

public record OrderCreatedEvent(Order Order) : IDomainEvent;