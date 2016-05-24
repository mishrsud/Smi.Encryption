using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Text;
using MGR.CommandLineParser.Command;
using System.Linq;
using System.Reflection;

namespace Smi.Encryption.CommandLine
{
	/// <summary> A command to generate API Key and secret </summary>
	/// <seealso cref="MGR.CommandLineParser.Command.CommandBase" />
	public class CredCommand : CommandBase
	{
		private readonly ApiCredentialManager _apiCredentialManager = ApiCredentialManager.Create();
		private readonly NameValueCollection _appSettingsCollection;

		/// <summary>
		/// Initializes a new instance of the <see cref="CredCommand"/> class.
		/// </summary>
		public CredCommand()
		{
			_appSettingsCollection = InitializeAppSettings();
		}

	    /// <summary> Creates a set of API Key and Secret on each call. </summary>
	    /// <returns>
	    /// Return 0 is everything was right, an negative error code otherwise.
	    /// </returns>
	    [Description("Creates a set of API Key and Secret on each call")]
		protected override int ExecuteCommand()
		{
			var credentials = _apiCredentialManager.GenerateAppCredentials();

			StringBuilder builder = new StringBuilder();
			builder.AppendLine($"API Key   : {credentials.ApiKey}");
			builder.AppendLine($"API Secret: {credentials.ApiSecret}");

			string authHeader = GetBase64Header(credentials.ApiKey, credentials.ApiSecret);

			Console.WriteLine("=========================================================================");
			Console.WriteLine(builder.ToString());
			Console.WriteLine("=========================================================================");
			Console.WriteLine("Authorization header: ");
			Console.WriteLine(authHeader);
			Console.WriteLine("=========================================================================");

			return 0;
		}

		/// <summary> Gets the Base64 encoded header. </summary>
		/// <param name="apiKey">The API key.</param>
		/// <param name="apiSecret">The API secret.</param>
		private string GetBase64Header(string apiKey, string apiSecret)
		{
			string headerString = string.Join(":", apiKey, apiSecret);
			var headerBytes = Encoding.ASCII.GetBytes(headerString);
			string header = Convert.ToBase64String(headerBytes);

			return string.Concat("Basic ", header);
		}

		/// <summary> Initializes the application settings. </summary>
		private NameValueCollection InitializeAppSettings()
		{
			NameValueCollection result = new NameValueCollection();

			if (
				ConfigurationManager.AppSettings.AllKeys.Length == 0 || 
				!ConfigurationManager.AppSettings.AllKeys.Contains("command.templateFileName"))
			{
				result["command.templateFileName"] = "ApiCredentialInsert.sql";
			}
			else
			{
				result = ConfigurationManager.AppSettings;
			}

			return result;
		}

		/// <summary> Gets the running directory. </summary>
		private static string GetRunningDirectory()
		{
			var location = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
			var directoryInfo = new FileInfo(location.AbsolutePath).Directory;
			if (directoryInfo != null)
			{
				return Uri.UnescapeDataString(directoryInfo.FullName);
			}

			throw new ApplicationException("Unable to determine running directory");
		}
	}
}
