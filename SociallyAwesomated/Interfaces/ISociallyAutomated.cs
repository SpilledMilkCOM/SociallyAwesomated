using System;
using System.Collections.Generic;

namespace SociallyAwesomated.Interfaces
{
	public interface ISociallyAutomated
	{
		Uri UserImage { get; }

		string UserName { get; }

		IEnumerable<IUser> GetFollowers(IUser user);

		void Post(IPost post);
	}
}