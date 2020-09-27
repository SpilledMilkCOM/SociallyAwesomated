using System.Collections.Generic;
using System.Net.Http;

namespace SM.Common.REST.Interfaces
{
	public interface IRestClientOptions
	{
		/// <summary>
		/// The content when using the POST method.
		/// </summary>
		public HttpContent Content { get; set; }

		/// <summary>
		/// Headers for the REST call.
		/// </summary>
		public IList<KeyValuePair<string, string>> Headers { get; set; }

		/// <summary>
		/// The HTTP Method to use (currently only Get and Post are supported)
		/// </summary>
		public HttpMethod HttpMethod { get; set; }

		/// <summary>
		/// The parameters for the full URL (key: parameter name, value: parameter value)
		/// </summary>
		public IList<KeyValuePair<string, string>> QueryStringValues { get; set; }

		/// <summary>
		/// The URL defining the endpoint (without the parameters)
		/// </summary>
		public string ServiceUrl { get; }

		public int Timeout { get; set; }
	}
}