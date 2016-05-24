using FluentAssertions;
using Xunit;

namespace Smi.Encryption.Test
{
    public class ApiCredentialManagerTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void ApiCredentialManager_Create_Produces_SingletonInstance()
        {
            var sut1 = ApiCredentialManager.Create();
            var sut2 = ApiCredentialManager.Create();

            sut2.Should().BeSameAs(sut1, "because we expect the class to be Singleton");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void ApiCredentialManager_CalltoGenerateApiCredentials_ProducesUniqueCredentialsAlways()
        {
            var sut = ApiCredentialManager.Create();
            ApiCredentials cred1 = sut.GenerateAppCredentials();

            ApiCredentials cred2 = sut.GenerateAppCredentials();

            cred2.ApiKey.Should().NotBe(cred1.ApiKey, "because we expect the credntials to be unique");
            cred2.ApiSecret.Should().NotBe(cred1.ApiSecret, "because we expect the credntials to be unique");
        }
    }
}
