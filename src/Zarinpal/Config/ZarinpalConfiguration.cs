

using Zarinpal.Enums;

namespace Zarinpal.Config
{
    public class PaymentMethodConfiguration
    {
        public PaymentMethodConfiguration(string merchantId, Currency currency, string basePaymentUrl)
        {
            MerchantId = merchantId;
            Currency = currency;
            BasePaymentUrl = basePaymentUrl;
        }

        public string MerchantId { get; private set; }
        public Currency Currency { get; private set; }
        public string BasePaymentUrl { get; private set; }
    }
}
