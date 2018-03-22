using SociallyAwesomated.Interfaces;

namespace SM.SociallyAwesomated
{
	public class TwitterPost : IPost
	{
		public string ImageUrl { get; set; }

		public string Post { get; set; }

		public IUser User { get; set; }
	}
}