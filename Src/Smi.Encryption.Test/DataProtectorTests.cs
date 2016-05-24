using System;
using FluentAssertions;
using Xunit;

namespace Smi.Encryption.Test
{
    public class DataProtectorTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void DataProtector_Create_MultipleCallsProduceSameInstance()
        {
            var sut1 = DataProtector.Create();
            var sut2 = DataProtector.Create();

            sut2.Should().BeSameAs(sut1, "beucase we expect the class to be singleton");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void DataProtector_Encrypt_WithNullInput_ThrowsArgumentNullException()
        {
            var sut = DataProtector.Create();
            sut.Invoking(protector => sut.Encrypt(null))
                .ShouldThrow<ArgumentNullException>("because we passed invalid data");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void DataProtector_Decrypt_WithNullInput_ThrowsArgumentNullException()
        {
            var sut = DataProtector.Create();
            sut.Invoking(protector => sut.Decrypt(null))
                .ShouldThrow<ArgumentNullException>("because we passed invalid data");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void DataProtector_Encrypt_ThenDecrypt_WithDefaultEntropy_ProducesExpectedClearText()
        {
            string clearText = "Hello World";
            var sut = DataProtector.Create();

            string cipherText = sut.Encrypt(clearText);
            string decrypted = sut.Decrypt(cipherText);

            decrypted.Should().Be(clearText, "becuase we expec the decryption to succeed");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void DataProtector_Encrypt_ThenDecrypt_WithCustomEntropy_ProducesExpectedClearText()
        {
            string clearText = "Hello World";
            var sut = DataProtector.Create(Guid.NewGuid().ToString("D").ToUpperInvariant());

            string cipherText = sut.Encrypt(clearText);
            string decrypted = sut.Decrypt(cipherText);

            decrypted.Should().Be(clearText, "becuase we expec the decryption to succeed");
        }
    }
}
