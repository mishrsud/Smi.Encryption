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
		/// <exception cref="System.NotImplementedException"></exception>
		[Description("Creates a set of API Key and Secret on each call")]
		protected override int ExecuteCommand()
		{
			var credentials = _apiCredentialManager.GenerateAppCredentials();
			string sqlTemplateText = GetSqlTemplate();
			sqlTemplateText = sqlTemplateText.Replace("{key}", credentials.ApiKey).Replace("{secret}", credentials.ApiSecret);

			StringBuilder builder = new StringBuilder();
			builder.AppendLine($"API Key   : {credentials.ApiKey}");
			builder.AppendLine($"API Secret: {credentials.ApiSecret}");

			string authHeader = GetBase64Header(credentials.ApiKey, credentials.ApiSecret);

			Console.WriteLine("=========================================================================");
			Console.WriteLine(builder.ToString());
			Console.WriteLine("=========================================================================");
			Console.WriteLine("SQL Script:");
			Console.WriteLine(sqlTemplateText);
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

		/// <summary> Gets the SQL template from the configured template file. </summary>
		/// <remarks>
		/// location of template file comes from app.config appSettings key command.templateFileName
		/// If this key is not found, we default to a file called ApiCredentialInsert.sql placed in the running 
		/// directory of the exe
		/// </remarks>
		/// <returns></returns>
		private string GetSqlTemplate()
		{
			var templateLocation = _appSettingsCollection["command.templateFileName"];

			if (!templateLocation.Contains(Path.VolumeSeparatorChar))
			{
				// this is an absolute filename, look for the file in the current running directory
				templateLocation = Path.Combine(GetRunningDirectory(), templateLocation);
			}

			string templateText = File.ReadAllText(templateLocation);
			return templateText;
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
