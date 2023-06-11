namespace SecretConnStrProviderSample {
    internal interface IConnectionStringProvider {
        string GetConnectionString(string name);
    }
}