using System;

namespace Smi.Encryption
{
	/// <summary> Provides methods to generate API credentials (key and secret) </summary>
	public class ApiCredentialManager
	{
		private static readonly ApiCredentialManager Instance = new ApiCredentialManager();

		/// <summary> Prevents a default instance of the <see cref="ApiCredentialManager"/> class from being created. </summary>
		private ApiCredentialManager()
		{
			
		}

		/// <summary> Creates an instance of <see cref="ApiCredentialManager"/>. </summary>
		public static ApiCredentialManager Create()
		{
			return Instance;
		}

		/// <summary>
		/// Generates the application credentials.
		/// </summary>
		/// <returns></returns>
		public ApiCredentials GenerateAppCredentials()
		{
			var appKey = Guid.NewGuid().ToString("N");
			var appSecret = RandomKeyGenerator.GenerateRandomKey(32);

			var appCredentials = new ApiCredentials
			{
				ApiKey = appKey,
				ApiSecret = appSecret
			};

			return appCredentials;
		}
	}
}
