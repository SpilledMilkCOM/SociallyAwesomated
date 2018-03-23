using SM.SociallyAwesomated;
using SM.Common.Interfaces;
using SociallyAwesomated.Interfaces;
using System;
using System.Collections.Generic;

// This is to diferentiate between the toolkit's TwitterUser and "my" TwitterUser.
using MUST = Microsoft.Toolkit.Uwp.Services.Twitter;

namespace SociallyAwesomated
{
	public class TwitterAutomated : ISociallyAutomated
	{
		// User that is logged in.
		private IUser _user;

		public TwitterAutomated(IOAuthCredentials credentials)
		{
			_user = new TwitterUser();

			InitializeTwitter(credentials);
		}

		public Uri UserImage { get; private set; }

		public string UserName { get; private set; }

		public IEnumerable<IUser> GetFollowers(IUser user)
		{
			return null;
		}

		public void Post(IPost post)
		{
			
		}

		private async void InitializeTwitter(IOAuthCredentials credentials)
		{
			try
			{
				//IsBusy = true;

				//Message = "Logging into Twitter...";

				MUST.TwitterService.Instance.Initialize(credentials.ConsumerKey, credentials.ConsumerSecret, credentials.CallbackUri);

				var user = await MUST.TwitterService.Instance.GetUserAsync();

				UserName = user.ScreenName;
				UserImage = new Uri(user.ProfileImageUrl);

				//Message = $"Logged in as {user.ScreenName}";

				//FetchTweets($"@{user.ScreenName}");
			}
			catch (Exception ex)
			{
				//ShowMessage(ex.Message);
			}
			finally
			{
				//IsBusy = false;
			}
		}
	}
}