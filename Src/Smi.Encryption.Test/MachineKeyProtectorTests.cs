using System;
using FluentAssertions;
using Xunit;

namespace Smi.Encryption.Test
{
    public class MachineKeyProtectorTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void MachineKey_Protect_WithNullText_ThrowsArgumentNullException()
        {
            var sut = new MachineKeyProtector();
            sut.Invoking(protector => sut.Protect(null, "test"))
                .ShouldThrow<ArgumentNullException>("because we passed invalid input");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void MachineKey_Protect_WithNullPurpose_ThrowsArgumentNullException()
        {
            var sut = new MachineKeyProtector();
            sut.Invoking(protector => sut.Protect("good", null))
                .ShouldThrow<ArgumentNullException>("because we passed invalid input");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void MachineKey_UnProtect_WithNullInput_ThrowsArgumentNullException()
        {
            var sut = new MachineKeyProtector();
            sut.Invoking(protector => sut.Unprotect(null, "good"))
                .ShouldThrow<ArgumentNullException>("because we passed invalid input");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void MachineKey_UnProtect_WithNullPurpose_ThrowsArgumentNullException()
        {
            var sut = new MachineKeyProtector();
            sut.Invoking(protector => sut.Unprotect("good", null))
                .ShouldThrow<ArgumentNullException>("because we passed invalid input");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void MachineKey_Protect_ProducesCipherText()
        {
            var sut = new MachineKeyProtector();
            string cipherText = sut.Protect("hello", Purposes.AuthToken);

            cipherText.Should().NotBe("hello", "because we expect the clear text to be encryted");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void MachineKey_Protect_ThenUnprotect_WithSamePurpose_Succeeds()
        {
            string clearText = "hello world";

            var sut = new MachineKeyProtector();
            string cipherText = sut.Protect(clearText, Purposes.ConnString);

            string decryptedText = sut.Unprotect(cipherText, Purposes.ConnString);
            decryptedText.Should()
                .Be(clearText, "because we expect the decrypted text to be same as the original clear text");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void MachineKey_Protect_ThenUnprotect_WithDifferentPurpose_ReturnsNull()
        {
            string clearText = "hello world";

            var sut = new MachineKeyProtector();
            string cipherText = sut.Protect(clearText, Purposes.ConnString);

            string decrypted = sut.Unprotect(cipherText, Purposes.AuthToken);
            decrypted.Should().BeNull("because we did not pass the expected purpose to decrypt");
        }
    }
}
