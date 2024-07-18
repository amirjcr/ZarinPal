namespace Zarinpal.Models;

/// <summary>
/// Incoming Request Model 
/// </summary>
public record PaymentRequest(int Amount, string Description, string CallBackUrl);




