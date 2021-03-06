﻿namespace SM.Common.Interfaces
{
	public interface IOAuthCredentials : IConfiguration
	{
		string AccessToken { get; set; }

		string AccessTokenSecret { get; set; }

		string CallbackUri { get; set; }

		string ConsumerKey { get; set; }

		string ConsumerSecret { get; set; }

		string Scope { get; set; }

		string State { get; set; }

		public string TokenUrl { get; set; }
	}
}