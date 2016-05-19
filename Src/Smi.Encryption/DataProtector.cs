using System;
using System.Security.Cryptography;
using System.Text;

namespace Smi.Encryption
{
	/// <summary> Encrypts and decrypts data using System.Security (DPAPI) </summary>
	public class DataProtector
	{
		private static readonly DataProtector Instance = new DataProtector();
		private const string DefaultEntropy = "F8D31315-1468-4C9D-82AD-DC2DEEA27E08";
		private static byte[] _entropyBytes;

		/// <summary> Prevents a default instance of the <see cref="DataProtector"/> class from being created. </summary>
		private DataProtector()
		{
			
		}

		/// <summary> Creates an instance of <see cref="DataProtector"/> with the specified entropy. </summary>
		/// <param name="entropy">The entropy.</param>
		/// <returns> An instance of <see cref="DataProtector"/></returns>
		public static DataProtector Create(string entropy = DefaultEntropy)
		{
			_entropyBytes = Encoding.Unicode.GetBytes(entropy);
			return Instance;
		}

		/// <summary> Encrypts the specified plain text. </summary>
		/// <param name="plainText">The plain text.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public string Encrypt(string plainText)
		{
			if (string.IsNullOrWhiteSpace(plainText)) throw new ArgumentNullException(nameof(plainText));

			byte[] plainTextBytes = Encoding.Unicode.GetBytes(plainText);
			byte[] encryptedBytes = ProtectedData.Protect(plainTextBytes, _entropyBytes, DataProtectionScope.LocalMachine);
			string encruptedText = Convert.ToBase64String(encryptedBytes);

			return encruptedText;
		}

		/// <summary> Decrypts the specified cipher text. </summary>
		/// <param name="cipherText">The cipher text.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		public string Decrypt(string cipherText)
		{
			if (string.IsNullOrWhiteSpace(cipherText)) throw new ArgumentNullException(nameof(cipherText));

			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			byte[] decryptedBytes = ProtectedData.Unprotect(cipherTextBytes, _entropyBytes, DataProtectionScope.LocalMachine);
			string decryptedText = Encoding.Unicode.GetString(decryptedBytes);

			return decryptedText;
		}
	}
}
