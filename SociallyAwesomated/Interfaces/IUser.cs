using System.Collections.Generic;

namespace SociallyAwesomated.Interfaces
{
	public interface IUser
	{
		string Id { get; set; }

		string Name { get; set; }

		IEnumerable<IUser> GetFollowers();
    }
}