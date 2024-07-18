

namespace Zarinpal.Server
{
    public class ServerConfiguration
    {
        public ServerConfiguration(List<string> endPoints)
        {
            EndPoints = endPoints;
            Port = 7074;
            HostName = "localhost";
        }

        public ServerConfiguration(List<string> endPoints, string hostName, int port)
        {
            EndPoints = endPoints;
            HostName = hostName;
            Port = port;
        }

        public List<string>? EndPoints { get; private set; }

        public string HostName { get; init; }
        public int Port { get; init; }


    }
}
