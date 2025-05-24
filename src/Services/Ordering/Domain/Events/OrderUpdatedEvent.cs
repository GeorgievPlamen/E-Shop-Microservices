using Domain.Abstractions;
using Domain.Models;

namespace Domain.Events;

public record OrderUpdatedEvent(Order Order) : IDomainEvent;