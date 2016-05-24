using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace Smi.Encryption
{
    [ExcludeFromCodeCoverage]
	public class HashDataHelper
	{
		/// <summary>
		/// Computes the SHA256 hash .
		/// </summary>
		/// <param name="toBeHashed">The byte array containing bytes to be hashed.</param>
		/// <returns></returns>
		public static byte[] ComputeHashSha256(byte[] toBeHashed)
		{
			using (var sha256 = SHA256.Create())
			{
				return sha256.ComputeHash(toBeHashed);
			}
		}

		/// <summary> Gets the SHA256 hash of the passed string. </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static string GetSha256Hash(string text)
		{
			using (SHA256Managed crypt = new SHA256Managed())
			{
				StringBuilder hash = new StringBuilder();
				byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
				foreach (byte bit in crypto)
				{
					hash.Append(bit.ToString("x2"));
				}

				return hash.ToString(); 
			}
		}
	}
}
