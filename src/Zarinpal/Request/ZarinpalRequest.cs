
namespace Zarinpal.Request;

/// <summary>
/// Incoming Request from user to connect ZarinPal
/// </summary>
public record ZarinpalRequest(int Amount, string Description, string CallBackUrl);


