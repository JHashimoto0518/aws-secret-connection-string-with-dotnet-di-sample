using System.Configuration;

namespace SecretConnStrProviderSample {
    internal class ConfigConnectionStringProvider : IConnectionStringProvider {
        public string GetConnectionString(string name) {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            return settings?.ConnectionString ?? throw new ConfigurationErrorsException($"{name} is not found in the configuration file.");
        }
    }
}
