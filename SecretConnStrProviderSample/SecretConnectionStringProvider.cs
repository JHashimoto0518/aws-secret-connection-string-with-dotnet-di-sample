using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace SecretConnStrProviderSample {
    internal class SecretConnectionStringProvider : IConnectionStringProvider {

        private readonly IAmazonSecretsManager _secretsManager;

        public SecretConnectionStringProvider(IAmazonSecretsManager secretsManager) {
            _secretsManager = secretsManager ?? throw new ArgumentNullException(nameof(secretsManager));
        }

        public async Task<string> GetConnectionString(string name) {
            try
            {
                var response = await _secretsManager.GetSecretValueAsync(new GetSecretValueRequest {
                    SecretId = name
                });

                // Parse the secret JSON string and construct the connection string for MySQL
                var secretData = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString);
                var connectionString = $"server={secretData["host"]};port={secretData["port"]};database={secretData["dbname"]};uid={secretData["username"]};pwd={secretData["password"]}";

                return connectionString;
            } catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve connection string from AWS Secrets Manager for secret {name}", ex);
            }
        }
    }
}
