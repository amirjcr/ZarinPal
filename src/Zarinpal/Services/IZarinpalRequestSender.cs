
using System.Net.Http;
using System.Net.Http.Json;
using Zarinpal.Config;
using Zarinpal.Models;
using Zarinpal.Request;

namespace Zarinpal.Services;
public interface IZarinpalRequestSender
{
    Task<string> SendAsync(ZarinpalRequest request, CancellationToken cancellationToken);
}


public class ZarinpalRequestSender : IZarinpalRequestSender
{

    private readonly HttpClient _httpClient;
    private readonly ZarinpalConfiguration _config;

    public ZarinpalRequestSender(HttpClient httpClient, ZarinpalConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<string> SendAsync(ZarinpalRequest request, CancellationToken cancellationToken)
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

