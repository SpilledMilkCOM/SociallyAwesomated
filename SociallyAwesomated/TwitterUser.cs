using System.Collections.Generic;
using SociallyAwesomated.Interfaces;

namespace SM.SociallyAwesomated
{
	public class TwitterUser : IUser
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public IEnumerable<IUser> GetFollowers()
		{
			throw new System.NotImplementedException();
		}
	}
}