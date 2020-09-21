namespace SM.Common.Interfaces
{
	public interface IOAuthCredentials : IConfiguration
	{
		string AccessToken { get; set; }

		string AccessTokenSecret { get; set; }

		string CallbackUri { get; set; }

		string ConsumerKey { get; set; }

		string ConsumerSecret { get; set; }
	}
}