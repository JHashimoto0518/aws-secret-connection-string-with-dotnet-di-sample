using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Moq;
using Xunit;

namespace SecretConnStrProviderSample.Test {
    public class SecretConnectionStringProviderTest {
        [Fact]
        public async void GetRDSConnectionString_ReturnsCorrectConnectionString() {
            // Arrange
            var mockSecretsManager = new Mock<IAmazonSecretsManager>();

            var secretString = @"{
                ""password"": ""dummy-password"",
                ""dbname"": ""dummy-database"",
                ""engine"": ""dummy-engine"",
                ""port"": 1234,
                ""dbInstanceIdentifier"": ""dummy-instance"",
                ""host"": ""dummy-host"",
                ""username"": ""dummy-username""
            }";

            mockSecretsManager
                .Setup(x => x.GetSecretValueAsync(It.IsAny<GetSecretValueRequest>(), default(CancellationToken)))
                .ReturnsAsync(new GetSecretValueResponse { SecretString = secretString });

            var provider = new SecretConnectionStringProvider(mockSecretsManager.Object);

            // Act
            var connectionString = await provider.GetConnectionString("dummy");

            // Assert
            Assert.Equal("server=dummy-host;port=1234;database=dummy-database;uid=dummy-username;pwd=dummy-password", connectionString);
        }
    }
}
