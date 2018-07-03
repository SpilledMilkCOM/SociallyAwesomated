using Microsoft.Toolkit.Parsers;
using Newtonsoft.Json;

namespace SociallyAwesomated.App.Models
{
	public class MyTwitterUser : SchemaBase
	{
		/// Gets or sets user Id.
		/// </summary>
		[JsonProperty("id_str")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets user name.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets user screen name.
		/// </summary>
		[JsonProperty("screen_name")]
		public string ScreenName { get; set; }

		/// <summary>
		/// Gets or sets user profile image Url.
		/// </summary>
		[JsonProperty("profile_image_url")]
		public string ProfileImageUrl { get; set; }
	}
}