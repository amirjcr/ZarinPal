
using System.Text.Json.Serialization;
using Zarinpal.Enums;

namespace Zarinpal.Models;

public class ZarinpalResponseDataModel
{
    public ZarinpalResponseDataModel(int code, ResponseMessageType message, string authority, int fee)
    {
        Code = code;
        Message = message;
        Authority = authority;
        Fee = fee;
    }

    [JsonPropertyName("code")]
    public int Code { get; init; }


    [JsonPropertyName("message")]
    public ResponseMessageType Message { get; init; }


    [JsonPropertyName("authority")]
    public string Authority { get; init; }


    [JsonPropertyName("fee")]
    public int Fee { get; init; }
}

