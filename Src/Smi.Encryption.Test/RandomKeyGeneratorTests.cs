using System;
using FluentAssertions;
using Xunit;

namespace Smi.Encryption.Test
{
    public class RandomKeyGeneratorTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Generator_WithZeroLength_ShouldThrow_ArgumentOutOfRangeException()
        {
            Action invalidAction = () => RandomKeyGenerator.GenerateRandomKey(0);
            invalidAction.ShouldThrow<ArgumentOutOfRangeException>("because we passed 0 as input");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Generator_WithNegativeLength_ShouldThrow_ArgumentOutOfRangeException()
        {
            Action invalidAction = () => RandomKeyGenerator.GenerateRandomKey(-1);
            invalidAction.ShouldThrow<ArgumentOutOfRangeException>("because we passed 0 as input");
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Generator_WithValidLength_ShouldCreateUniqueKeyOnEachCall()
        {
            string random1 = RandomKeyGenerator.GenerateRandomKey(32);
            string random2 = RandomKeyGenerator.GenerateRandomKey(32);

            random1.Should().NotBe(random2, "beucase we expect each call to create unique keys");
        }
    }
}
