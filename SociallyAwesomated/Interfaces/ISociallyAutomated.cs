using System.Collections.Generic;

namespace SociallyAwesomated.Interfaces
{
	public interface ISociallyAutomated
	{
		IEnumerable<IUser> GetFollowers(IUser user);

		void Post(IPost post);
	}
}