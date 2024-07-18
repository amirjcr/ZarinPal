

using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json.Serialization.Metadata;
using Zarinpal.Models;
using Zarinpal.Request;

namespace Zarinpal.Services
{
    /// <summary>
    /// Represet a Handler for Payment Methods On Programms which does not Have External Server To Redierct User to Payment Gateways
    /// </summary>
    public interface IZarinPalRequestHandler
    {
        /// <summary>
        /// Handle Incomming Request and Redirect User Payment Gateways
        /// </summary>
        /// <param name="paymentRequest">Icommign Request</param>
        Task Handle(PaymentRequest paymentRequest, CancellationToken cancellationToken = default);
    }

    public class ZarinPalRequestHandler : IZarinPalRequestHandler
    {
        private readonly IClientRequest _clientRequest;
        private readonly ILogger<ZarinPalRequestHandler> _looger;

        public ZarinPalRequestHandler(IClientRequest clientRequest, ILogger<ZarinPalRequestHandler> looger)
        {
            _clientRequest = clientRequest;
            _looger = looger;
        }

        ///<inheritdoc/>
        public async Task Handle(PaymentRequest paymentRequest, CancellationToken cancellationToken)
        {
            try
            {
                _looger.LogInformation("Initiate PaymentRequest");
                var paymentUrl = await _clientRequest.InitiateRequest(paymentRequest, cancellationToken);

                _looger.LogInformation("Opoen Default Browser");
                Process.Start(paymentUrl);
            }
            catch (Exception ex)
            {
                _looger.LogError(ex.Message, ex);
            }
        }
    }
}
