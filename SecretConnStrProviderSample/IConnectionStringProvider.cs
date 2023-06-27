namespace SecretConnStrProviderSample {
    internal interface IConnectionStringProvider {
        Task<string> GetConnectionString(string name);
    }
}