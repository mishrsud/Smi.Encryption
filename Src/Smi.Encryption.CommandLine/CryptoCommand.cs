using System;
using System.ComponentModel.DataAnnotations;
using MGR.CommandLineParser.Command;

namespace Smi.Encryption.CommandLine
{
	/// <summary> Represents a command that can perform encryption and decryption </summary>
	/// <seealso cref="MGR.CommandLineParser.Command.CommandBase" />
	public class CryptoCommand : CommandBase
	{
		/// <summary> Gets or sets the option that determines the operation to perform. </summary>
		[Display(ShortName = "op", Description = "The option to call. e for encrypt, d for decrypt")]
		[Required]
		public string Option { get; set; }

		/// <summary> Gets or sets the text. </summary>
		[Display(ShortName = "tx", Description = "The text to encrypt or decrypt. Enclose within double quotes")]
		[Required]
		public string Text { get; set; }

		/// <summary> Gets or sets the entropy. </summary>
		[Display(ShortName = "ep", Description = "An entropy for encyption. Default would be used if not passed")]
		public string Entropy { get; set; }

		/// <summary> Executes the command. </summary>
		/// <returns> Return 0 is everything was right, an negative error code otherwise. </returns>
		protected override int ExecuteCommand()
		{
			if (Option.Equals("e", StringComparison.OrdinalIgnoreCase))
			{
				var protector = string.IsNullOrWhiteSpace(Entropy) ? DataProtector.Create() : DataProtector.Create(Entropy);
				try
				{
					var encrypted = protector.Encrypt(Text);
					Console.WriteLine("Encrypted: {0}", encrypted);
					return 0;
				}
				catch (Exception ex)
				{
					string message = ex.Message;
					Console.WriteLine("An error was encountered: {0} {1}Details: {2}", message, Environment.NewLine, ex.StackTrace);
					return -1;
				}
			}

			if (Option.Equals("d", StringComparison.OrdinalIgnoreCase))
			{
				var protector = string.IsNullOrWhiteSpace(Entropy) ? DataProtector.Create() : DataProtector.Create(Entropy);
				try
				{
					var decrypted = protector.Decrypt(Text);
					Console.WriteLine("Decrypted: {0}", decrypted);
					return 0;
				}
				catch (Exception ex)
				{
					string message = ex.Message;
					Console.WriteLine("An error was encountered: {0} {1}Details: {2}", message, Environment.NewLine, ex.StackTrace);
					return -1;
				}
			}

			Console.WriteError("Invalid option value. Supported options are e OR d");
			return -1;
		}
	}
}
