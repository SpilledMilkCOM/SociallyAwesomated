using Microsoft.VisualStudio.TestTools.UnitTesting;
using SM.UWP.Common;

namespace SociallyAwesomated.Tests.Common
{
	[TestClass]
	// Compiler is saying this attribute is "internal" and cannot access due to the protection level.
	//[DeploymentItem("OAuth.default.json")]		// File is marked as "content" - Copy if newer.
	public class OAuthCredentialLoaderTests
	{
		[TestMethod, TestCategory("Mock")]
		public void OAuthCredentialLoader_Construction()
		{
			var test = new OAuthCredentialLoader();

			Assert.IsNotNull(test, "Unable to construct OAuthCredentialLoader");
		}

		[TestMethod, TestCategory("Integration")]
		public void OAuthCredentialLoader_Load()
		{
			var test = new OAuthCredentialLoader();

			var oathConfiguration = test.Load("OAuth.default.json");        // File is marked as "content" - Copy if newer.

			Assert.AreEqual("asdfghjkl AccessToken asdfghjkl", oathConfiguration.AccessToken, "The AccessToken did not match.");
			Assert.AreEqual("asdfghjkl AccessTokenSecret asdfghjkl", oathConfiguration.AccessTokenSecret, "The AccessTokenSecret did not match.");
			Assert.AreEqual("http://www.SpilledMilk.com", oathConfiguration.CallbackUri, "The CallbackUri did not match.");
			Assert.AreEqual("asdfghjkl ConsumerKey asdfghjkl", oathConfiguration.ConsumerKey, "The ConsumerKey did not match.");
			Assert.AreEqual("asdfghjkl ConsumerSecret asdfghjkl", oathConfiguration.ConsumerSecret, "The ConsumerSecret did not match.");
		}
	}
}