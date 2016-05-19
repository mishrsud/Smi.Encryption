using System;
using System.Security.Cryptography;

namespace Smi.Encryption
{
	public class RandomKeyGenerator
	{
		/// <summary> Generates a Cryptographicallty secure random key. </summary>
		/// <param name="length">The length.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">length should be greater than 0</exception>
		/// <remarks> For a 256 bit key, pass the length as 32 (32 bytes * 8 bits = 256 bits) </remarks>
		public static string GenerateRandomKey(int length)
		{
			if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length), "length should be greater than 0");

			using (var randomNumberGenerator = new RNGCryptoServiceProvider())
			{
				var randomNumber = new byte[length];
				randomNumberGenerator.GetBytes(randomNumber);

				return Convert.ToBase64String(randomNumber);
			}
		}
	}
}
