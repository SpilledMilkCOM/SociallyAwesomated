using SM.Common.Interfaces;

namespace SM.Common
{
	public class OAuthCredentials : IOAuthCredentials
	{
		public string AccessToken { get; set; }

		public string AccessTokenSecret { get; set; }

		public string CallbackUri { get; set; }

		public string ConsumerKey { get; set; }

		public string ConsumerSecret { get; set; }
	}
}