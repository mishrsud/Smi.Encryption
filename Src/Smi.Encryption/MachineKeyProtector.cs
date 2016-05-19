using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Smi.Encryption
{
	/// <summary> Encrypts and decrypts text based on the Machine key </summary>
	public class MachineKeyProtector
	{
		/// <summary> Protects the specified plain text. </summary>
		/// <param name="plainText">The plain text.</param>
		/// <param name="purpose">The purpose.</param>
		/// <returns> string Encrypted based on machine key</returns>
		public string Protect(string plainText, string purpose)
		{
			if (string.IsNullOrWhiteSpace(plainText)) throw new ArgumentNullException(nameof(plainText));
			if (string.IsNullOrWhiteSpace(purpose)) throw new ArgumentNullException(nameof(purpose));

			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			var protectedBytes = MachineKey.Protect(plainTextBytes, purpose);
			var protectedText = Convert.ToBase64String(protectedBytes);
			return protectedText;
		}

		/// <summary> Unprotects the specified protected text. </summary>
		/// <param name="protectedText">The protected text.</param>
		/// <param name="purpose">The purpose.</param>
		/// <returns>Plain text decrypted using machine key OR null</returns>
		public string Unprotect(string protectedText, string purpose)
		{
			if (string.IsNullOrWhiteSpace(protectedText)) throw new ArgumentNullException(nameof(protectedText));
			if (string.IsNullOrWhiteSpace(purpose)) throw new ArgumentNullException(nameof(purpose));

			var protectedBytes = Convert.FromBase64String(protectedText);
			string result = null;

			try
			{
				var plainTextBytes = MachineKey.Unprotect(protectedBytes, purpose);
				if (plainTextBytes != null)
				{
					result = Encoding.UTF8.GetString(plainTextBytes);
				}
			}
			catch (CryptographicException)
			{
				return null;
			}
			
			return result;
		}
	}
}
