using System;

namespace SM.OAuth2
{
	/// <summary>
	/// This is the response data from the OAuth2 "token" endpoint.
	/// </summary>
	[Serializable]
	public class TokenResponse
	{
		private DateTime? _expiresAt = null;
		private int _expiresIn;

		/// <summary>
		/// The Access Token to send the OAuth2 endpoint in the "Authorization" header
		/// </summary>
		public string access_token { get; set; }

		/// <summary>
		/// The number of seconds the key is valid.
		/// </summary>
		public int expires_in
		{
			get
			{
				return _expiresIn;
			}
			set
			{
				// Subtract out a buffer to make sure there's no weird delay.
				// You may lose out on 5 seconds, but you will get a nice fresh key sooner and you're more certain you won't use an expired key.

				_expiresIn = value;
				_expiresAt = DateTime.Now.AddSeconds(value - 2);
			}
		}

		/// <summary>
		/// The type of token to be sent in the "Authorization" header.
		/// </summary>
		public string token_type { get; set; }

		/// <summary>
		/// Has the token expired? (does it even exist?)
		/// </summary>
		public bool IsExpired { get => !_expiresAt.HasValue || _expiresAt.Value > DateTime.Now; }
	}
}