using SM.Common.Interfaces;
using SM.Common.REST.Interfaces;
using SM.Common.Serialization;
using SM.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace SM.OAuth2
{
	public class OAuth3LeggedAuthenticator : IAuthenticator
	{
		private readonly IRestClient _client;
		private readonly IOAuthCredentials _credentials;
		private readonly ISerializationUtility _serializationUtility;

		public OAuth3LeggedAuthenticator(IRestClient client, IOAuthCredentials credentials, ISerializationUtility serializationUtility)
		{
			_client = client;
			_credentials = credentials;
			_serializationUtility = serializationUtility;
		}

		public void Authenticate()
		{
			string responseData = null;
			//			var urlRegex = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$");
			var urlRegex = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)");
			var match = urlRegex.Match(_credentials.TokenUrl);

			if (match.Success)
			{
				_client.BaseAddress = match.Value;
				_client.EndpointMethod = _credentials.TokenUrl.Replace(_client.BaseAddress, string.Empty);
			}

			// https://docs.microsoft.com/en-us/linkedin/shared/authentication/authorization-code-flow?context=linkedin/context

			_client.AddParameter("response_type", "code");
			_client.AddParameter("client_id", _credentials.ConsumerKey);
			_client.AddParameter("redirect_uri", _credentials.CallbackUri);
			_client.AddParameter("state", _credentials.State);
			_client.AddParameter("scope", _credentials.Scope);

			responseData = _client.Get();

			if (responseData != null)
			{
				var tokenResponse = _serializationUtility.Deserialize<TokenResponse>(responseData);

				if (!string.IsNullOrEmpty(tokenResponse.error))
				{
					throw new Exception($"Authentication Error: {tokenResponse.error} - {tokenResponse.error_description}");
				}

				_credentials.AccessToken = tokenResponse.access_token;
			}
		}
	}
}
