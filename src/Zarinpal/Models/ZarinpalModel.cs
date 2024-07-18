
using System.Text.Json.Serialization;
using Zarinpal.Enums;

namespace Zarinpal.Models;

public class ZarinPalModel
{
    #region Constractors
    public ZarinPalModel(string merchantId, Currency currency, int amount, string callBackUrl,
                         string description)
    {
        Gurad(amount, callBackUrl, merchantId);

        Currency = currency;
        MerchantId = merchantId;
        Amount = amount;
        CallBackUrl = callBackUrl;
        Description = description;
        MetaData = default;
    }

    public ZarinPalModel(string merchantId, Currency currency, int amount, string callBackUrl,
                         string description, MetaData? metaData)
    {
        Gurad(amount, callBackUrl, merchantId);


        Currency = currency;
        MerchantId = merchantId;
        Amount = amount;
        CallBackUrl = callBackUrl;
        Description = description;
        MetaData = metaData;
    }
    #endregion


    #region props

    [JsonPropertyName("merchant_id")]
    public string MerchantId { get; init; }

    [JsonPropertyName("amount")]
    public int Amount { get; init; }


    [JsonPropertyName("currency")]
    public Currency Currency { get; init; }



    [JsonPropertyName("callback_url")]
    public string CallBackUrl { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("metadata")]
    public MetaData? MetaData { get; init; }


    #endregion

    private void Gurad(int amount, string callBackUrl, string merchantId)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (string.IsNullOrWhiteSpace(callBackUrl))
            throw new ArgumentNullException(nameof(callBackUrl));

        if (string.IsNullOrWhiteSpace(merchantId))
            throw new ArgumentNullException(nameof(merchantId));
    }
}

public class MetaData
{
    public MetaData(string mobile, string email)
    {
        Guard(mobile, email);

        Mobile = mobile;
        this.email = email;
    }

    [JsonPropertyName("mobile")]
    public string Mobile { get; init; }

    [JsonPropertyName("email")]
    public string email { get; init; }


    private void Guard(string mobile, string email)
    {
        if (string.IsNullOrWhiteSpace(mobile))
            throw new ArgumentNullException(nameof(mobile));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));
    }
}
