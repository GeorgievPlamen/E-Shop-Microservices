namespace Domain.ValueObjects;

public record Payment(
    string? CardName,
    string CardNumber,
    string Expiration,
    string CVV,
    int PaymentMethod);