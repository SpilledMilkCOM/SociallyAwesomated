using SociallyAwesomated.Interfaces;
using System.Collections.Generic;

namespace SociallyAwesomated
{
	public class TwitterAutomated : ISociallyAutomated
	{
		public IEnumerable<IUser> GetFollowers(IUser user)
		{
			throw new System.NotImplementedException();
		}

		public void Post(IPost post)
		{
			throw new System.NotImplementedException();
		}
	}
}