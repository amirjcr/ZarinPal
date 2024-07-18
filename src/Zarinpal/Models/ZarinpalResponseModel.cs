
using System.Text.Json.Serialization;

namespace Zarinpal.Models;

public class ZarinpalResponseModel
{
    public ZarinpalResponseModel(ZarinpalResponseDataModel data, string errors)
    {
        Data = data;
        Errors = errors;
    }

    [JsonPropertyName("data")]
    public ZarinpalResponseDataModel Data { get; init; }

    [JsonPropertyName("errors")]
    public string Errors { get; init; }
}
