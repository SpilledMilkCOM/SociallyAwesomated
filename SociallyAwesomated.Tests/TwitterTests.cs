using Microsoft.Toolkit.Uwp.Services.Twitter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SM.Common;
//using SociallyAwesomated.App.ViewModels;

namespace SociallyAwesomated.Tests.Common
{
	[TestClass]
	// Compiler is saying this attribute is "internal" and cannot access due to the protection level.
	// The UWP notes is saying that this 
	//[DeploymentItem("OAuth.default.secret.json")]		// File is marked as "content" - Copy if newer.
	public class TwitterTests
	{
		[TestMethod, TestCategory("Integration")]
		public void TwitterTests_Logon()
		{
			TwitterTests_LogonAsync();
		}

		private async void TwitterTests_LogonAsync()
		{
			var loader = new OAuthCredentialLoader();

			Assert.IsNotNull(loader, "Unable to construct OAuthCredentialLoader");

			var credentials = loader.Load("OAuth.default.secret.json");

			Assert.IsNotNull(credentials);
			Assert.AreEqual("http://checkoutmystuff.net/", credentials.CallbackUri);

			var twitterAuth = new TwitterOAuthTokens
			{
				AccessToken = credentials.AccessToken,
				AccessTokenSecret = credentials.AccessTokenSecret,
				CallbackUri = credentials.CallbackUri,
				ConsumerKey = credentials.ConsumerKey,
				ConsumerSecret = credentials.ConsumerSecret
			};
			TwitterService.Instance.Initialize(twitterAuth);

			//TwitterService.Instance.Initialize(credentials.ConsumerKey, credentials.ConsumerSecret, credentials.CallbackUri);

			// Login to Twitter
			var loggedIn = await TwitterService.Instance.LoginAsync();

			Assert.IsTrue(loggedIn, "FAILED to login.");

			var user = await TwitterService.Instance.GetUserAsync();

			Assert.IsNotNull(user, "FAILED to get the user.");
		}

		//[TestMethod, TestCategory("Integration")]
		//public void TwitterTests_Followers()
		//{
		//	var credentials = new CredentialsViewModel();

		//	Assert.IsNotNull(credentials);

		//	credentials.ConnectCommand.Execute(null);

		//	var followers = new TwitterFollowersViewModel();

		//	Assert.IsNotNull(followers);
		//}
	}
}