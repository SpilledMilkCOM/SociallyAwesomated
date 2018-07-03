using SM.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SociallyAwesomated.App
{
	/// <summary>
	/// This MenuModel is the mapping between text and view Types; there is NO knowledge of any UI specific values.
	/// </summary>
    public class MenuModel : IMenuModel
    {
		public const string FACEBOOK_EVENTS = "Facebook.Events";
		public const string FACEBOOK_FOLLOWERS = "Facebook.Followers";
		public const string HOME = "home";
		public const string TWITTER_EVENTS = "Twitter.Events";
		public const string TWITTER_FOLLOWERS = "Twitter.Followers";
		public const string SETTINGS = "settings";

		private readonly Dictionary<string, Type> _viewMap;

		public MenuModel()
		{
			_viewMap = new Dictionary<string, Type>();

			_viewMap.Add(SETTINGS, typeof(Credentials));
			_viewMap.Add(FACEBOOK_EVENTS, typeof(FacebookEvents));
			_viewMap.Add(FACEBOOK_FOLLOWERS, typeof(FacebookEvents));
			_viewMap.Add(HOME, typeof(Home));
			_viewMap.Add(TWITTER_EVENTS, typeof(TwitterEvents));
			_viewMap.Add(TWITTER_FOLLOWERS, typeof(TwitterFollowers));
		}

		public string SettingsMap => SETTINGS;

		public Type Map(string name)
		{
			Type result = null;

			if (_viewMap.ContainsKey(name))
			{
				result = _viewMap[name];
			}

			return result;
		}

		public string Map(Type type)
		{
			var pair = _viewMap.ToList().FirstOrDefault(item => item.Value == type);
			string result = null;

			// KeyValuePair is a struct and is NOT null, so compare against default()

			if (!pair.Equals(default(KeyValuePair<string, Type>)))
			{
				result = pair.Key;
			}

			return result;
		}
	}
}