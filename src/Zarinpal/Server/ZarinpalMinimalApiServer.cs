
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using Zarinpal.Models;

namespace Zarinpal.Server;


public class ZarinpalMinimalApiServer
{
    private readonly static HttpListener _httpListener = new HttpListener();
    private static Thread? _thread;
    private static ServerConfiguration? _serverConfiguration;
    private readonly static List<Exception> _errors = new List<Exception>();
    private readonly static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private static CancellationToken _cancellationToken = _cancellationTokenSource.Token;


    public static event EventHandler<ZarinpalResponseModel>? RequestRecevied;
    public static event EventHandler<Exception>? HandleErros;


    public static void EstablishMinimalHttpServer(ServerConfiguration serverConfiguration)
    {
        if (serverConfiguration == null)
            throw new Exception("Server Configuration Must Be Set");

        _serverConfiguration = serverConfiguration;

        if (_httpListener == null) throw new InvalidOperationException("Can Not Establish HttpServer");
        _httpListener.Prefixes.Add($"http://{serverConfiguration.HostName}:{serverConfiguration.Port}");

        Trace.WriteLine($"Http listener Configuraed htpp://{serverConfiguration.HostName}:{serverConfiguration.Port}");
    }

    public static void StartServer()
    {
        if (_serverConfiguration == null)
            throw new Exception("Server Configuration Must Be Set");

        if (_serverConfiguration.EndPoints == null || _serverConfiguration.EndPoints.Count() == 0)
            throw new Exception("Can not Establish Server , Endpoints are not sets");

        _thread = new Thread(() => HandleInCommingRequest(_serverConfiguration.EndPoints));
        _thread.IsBackground = true;
        _thread.Name = "Minimal Http Server Thread";
        _thread.Priority = ThreadPriority.Normal;
        _thread.Start();
    }

    public static void StopServer()
    {
        _httpListener?.Stop();
        _httpListener?.Close();

        _cancellationTokenSource.Cancel();

    }



    private static void HandleInCommingRequest(List<string> endpoints)
    {
        while (true)
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                _errors.Add(new Exception($"{_thread?.Name} is stoped  and http server shut down"));
                _cancellationToken.ThrowIfCancellationRequested();
            }

            try
            {
                var context = _httpListener.GetContext();

                if (context != null)
                    if (context.Request.HttpMethod == HttpMethod.Post.ToString())
                    {
                        var request = context.Request;


                        foreach (var endpoint in endpoints)
                            if (request.RawUrl == endpoint && request.HasEntityBody)
                            {
                                var buffer = new byte[request.InputStream.Length];

                                using var stream = request.InputStream;
                                stream.Read(buffer, 0, (int)request.InputStream.Length);

                                var zarinPalResponse = JsonSerializer.Deserialize<ZarinpalResponseModel>(buffer);

                                if (zarinPalResponse != null)
                                    RequestRecevied?.Invoke(null, zarinPalResponse);
                            }

                    }
            }
            catch (Exception ex)
            {
                _errors.Add(ex);
                HandleErros?.Invoke(default, ex);
            }
        }
    }

}

