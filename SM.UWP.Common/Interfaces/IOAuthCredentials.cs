namespace SM.UWP.Common.Interfaces
{
	public interface IOAuthCredentials
	{
		string AccessToken { get; set; }

		string AccessTokenSecret { get; set; }

		string CallbackUri { get; set; }

		string ConsumerKey { get; set; }

		string ConsumerSecret { get; set; }
	}
}