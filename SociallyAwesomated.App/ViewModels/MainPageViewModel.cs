using SM.UWP.Common;
using SociallyAwesomated.Interfaces;

namespace SociallyAwesomated.App.ViewModels
{
	public class MainPageViewModel
	{
		private ISociallyAutomated _social = null;

		public MainPageViewModel()
			: this(new TwitterAutomated(new OAuthCredentialLoader().Load("OAuth.default.secret.json")))
		{

		}

		public MainPageViewModel(ISociallyAutomated social)
		{
			// TODO: Use some sort of injection or IoC

			_social = social;
		}
	}
}