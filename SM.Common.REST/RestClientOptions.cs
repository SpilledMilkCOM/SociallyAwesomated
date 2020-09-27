using SM.Common.REST.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;

namespace SM.Common.REST
{
	public class RestClientOptions : IRestClientOptions
	{
		private string _completeServiceUrl;

		public RestClientOptions()
		{
			Timeout = 60000;
			HttpMethod = HttpMethod.Get;
			QueryStringValues = new List<KeyValuePair<string, string>>();
		}

		public RestClientOptions(string serviceUrl)
		{
			ServiceUrl = serviceUrl;
		}

		public HttpContent Content { get; set; }

		public IList<KeyValuePair<string, string>> Headers { get; set; }

		public HttpMethod HttpMethod { get; set; }

		public IList<KeyValuePair<string, string>> QueryStringValues { get; set; }

		public string ServiceUrl { get; private set; }

		public int Timeout { get; set; }

		/// <summary>
		/// Get a full URL from the service parameters (only creates ONCE).
		/// </summary>
		/// <returns>A full URL including parameters.</returns>
		public string CompleteServiceUrl()
		{
			if (_completeServiceUrl != null)
			{
				return _completeServiceUrl;
			}

			var completeServiceUrlBuilder = new StringBuilder(ServiceUrl);

			if (QueryStringValues != null)
			{
				var nextDelimiter = "?";
				foreach (var kvp in QueryStringValues)
				{
					completeServiceUrlBuilder.Append(nextDelimiter);
					nextDelimiter = "&";

					completeServiceUrlBuilder.Append(HttpUtility.UrlEncode(kvp.Key));
					completeServiceUrlBuilder.Append("=");
					completeServiceUrlBuilder.Append(HttpUtility.UrlEncode(kvp.Value));
				}
			}

			return _completeServiceUrl = completeServiceUrlBuilder.ToString();
		}
	}
}