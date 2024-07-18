using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using Zarinpal.Config;
using Zarinpal.Models;

namespace Zarinpal.Request;


/// <summary>
/// Represent Incoming Request From client To Handler Section 
/// </summary>
public interface IClientRequest
{
    /// <summary>
    /// Received Client Request and Initiate Payment 
    /// </summary>
    /// <param name="request">Incomming Request</param>
    /// <returns>Returns Address to Redirect To payment Gateway or Throw Exception</returns>
    Task<string> InitiateRequest(PaymentRequest request, CancellationToken cancellationToken = default);
}

public class ClientRequest : IClientRequest
{
    private readonly HttpClient _httpClient; // must impelemented as singleton  
    private readonly PaymentMethodConfiguration _config;
    private bool _disposed = false;

    public ClientRequest(HttpClient httpClient, PaymentMethodConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    ///<inheritdoc/>
    public async Task<string> InitiateRequest(PaymentRequest request, CancellationToken cancellationToken = default)
    {
        var model = new ZarinPalModel(_config.MerchantId, _config.Currency, request.Amount, request.CallBackUrl, request.Description);
        var reponse = await _httpClient.PostAsJsonAsync<ZarinPalModel>(string.Empty, model, cancellationToken);


        var content = await reponse.Content.ReadFromJsonAsync<ZarinpalResponseModel>(cancellationToken);

        if (content?.Data.Message == Enums.ResponseMessageType.Success)
            return $"{_config.BasePaymentUrl}/{content?.Data.Authority}";
        else
            throw new InvalidOperationException("Can not Get Success Respoonse From ZarinPal Servers for current Request",
                new Exception($"Request Info  : Amount : {request.Amount} \n CallBack Url : {request.CallBackUrl} \n Description : {request.Description}"));
    }



}





