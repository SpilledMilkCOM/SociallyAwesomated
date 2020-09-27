using SM.Common.Interfaces;
using SM.Common.REST.Interfaces;
using SM.Common.Serialization;
using SM.Interfaces;
using System.Text.RegularExpressions;

namespace SM.OAuth2
{
	public class OAuth2Authenticator : IAuthenticator
	{
		private readonly IRestClient _client;
		private readonly IOAuthCredentials _credentials;
		private readonly ISerializationUtility _serializationUtility;

		public OAuth2Authenticator(IRestClient client, IOAuthCredentials credentials, ISerializationUtility serializationUtility)
		{
			_client = client;
			_credentials = credentials;
			_serializationUtility = serializationUtility;
		}

		public void Authenticate()
		{
//			var urlRegex = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$");
			var urlRegex = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)");
			var match = urlRegex.Match(_credentials.TokenUrl);

			if (match.Success)
			{
				_client.BaseAddress = match.Value;
				_client.EndpointMethod = _credentials.TokenUrl.Replace(_client.BaseAddress, string.Empty);
			}

			_client.ContentType = "application/x-www-form-urlencoded";

			//&scope={OAuthClientScope}
			var responseData = _client.Post($"grant_type=client_credentials&client_id={_credentials.ConsumerKey}&client_secret={_credentials.ConsumerSecret}");

			if (responseData != null)
			{
				var tokenResponse = _serializationUtility.Deserialize<TokenResponse>(responseData);

				_credentials.AccessToken = tokenResponse.access_token;
			}
		}
	}
}
