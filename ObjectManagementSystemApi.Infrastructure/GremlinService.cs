using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace ObjectManagementSystemApi.Infrastructure
{
    public class GremlinService : IDisposable
    {
        // Azure Cosmos DB Configuration variables
        private static string Host => Environment.GetEnvironmentVariable("Host") ?? throw new ArgumentException("Missing env var: Host");
        private static string PrimaryKey => Environment.GetEnvironmentVariable("PrimaryKey") ?? throw new ArgumentException("Missing env var: PrimaryKey");
        private static string Database => Environment.GetEnvironmentVariable("DatabaseName") ?? throw new ArgumentException("Missing env var: DatabaseName");
        private static string Container => Environment.GetEnvironmentVariable("ContainerName") ?? throw new ArgumentException("Missing env var: ContainerName");

        private readonly GremlinClient gremlinClient;

        public GremlinService()
        {
            string containerLink = "/dbs/" + Database + "/colls/" + Container;

            var gremlinServer = new GremlinServer(Host, 443, enableSsl: true,
                                                    username: containerLink,
                                                    password: PrimaryKey);

            ConnectionPoolSettings connectionPoolSettings = new ConnectionPoolSettings()
            {
                MaxInProcessPerConnection = 10,
                PoolSize = 30,
                ReconnectionAttempts = 3,
                ReconnectionBaseDelay = TimeSpan.FromMilliseconds(500)
            };

            var webSocketConfiguration =
            new Action<ClientWebSocketOptions>(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(10);
            });

            gremlinClient = new GremlinClient(
                gremlinServer,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType,
                connectionPoolSettings,
                webSocketConfiguration);
        }

        public async Task<string> SubmitRequest(string query)
        {
            var resultSet = await gremlinClient.SubmitAsync<dynamic>(query);

            return JsonConvert.SerializeObject(resultSet);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                gremlinClient?.Dispose();
            }
        }
    }
}