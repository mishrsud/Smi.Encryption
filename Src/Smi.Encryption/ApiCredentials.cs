namespace Smi.Encryption
{
	/// <summary> Holds API Key and secret </summary>
	public struct ApiCredentials
	{
		/// <summary> Gets the API key. </summary>
		public string ApiKey { get; internal set; }

		/// <summary> Gets the API secret. </summary>
		public string ApiSecret { get; internal set; }
	}
}
