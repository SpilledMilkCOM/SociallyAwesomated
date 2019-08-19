using Microsoft.Toolkit.Uwp.Connectivity;
using Microsoft.Toolkit.Uwp.Services.Twitter;
using SM.Common;
using SM.Common.Interfaces;
using SM.Common.ViewModels;
using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SociallyAwesomated.App.ViewModels
{
	public class CredentialsViewModel : ViewModelBase
	{
		private const string ATTAG = "@";

		private Visibility _busyVisibility;
		private string _callbackUrl;
		private string _consumerKey;
		private string _consumerSecrect;
		private string _message;
		private Brush _messageBrush;
		private Visibility _messageVisibility;
		private IOAuthCredentials _oauthCredentials;
		private Uri _userImage;
		private string _userScreenName;

		public CredentialsViewModel()
		{
			if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
			{
				var test = new OAuthCredentialLoader();

				// TODO: File name from configuration.

				try
				{
					_oauthCredentials = test.Load("OAuth.default.secret.json");        // File is marked as "content" - Copy if newer.
				}
				catch (Exception ex)
				{
					throw ex;
				}

				BusyVisibility = ToVisibility(false);

				ConnectCommand = new RelayCommand(Connect, CanConnect);
			}
			else
			{
				// Some design-time data.

				_oauthCredentials = new OAuthCredentials
				{
					AccessToken = "asdfghjkl Put your cryptic token here asdfghjkl",
					AccessTokenSecret = "asdfghjkl Put your cryptic token here asdfghjkl",
					CallbackUri = "http://www.SpilledMilk.com",
					ConsumerKey = "asdfghjkl Put your cryptic token here asdfghjkl",
					ConsumerSecret = "asdfghjkl Put your cryptic token here asdfghjkl"
				};

				BusyVisibility = ToVisibility(true);

				UserImage = new Uri("http://checkoutmystuff.net/Images/Check Out My Stuff Logo.png");
			}

			CallbackUrl = _oauthCredentials?.CallbackUri;
			ConsumerKey = _oauthCredentials?.ConsumerKey;
			ConsumerSecrect = _oauthCredentials?.ConsumerSecret;

			//if (!IsDesignModeEnabled)
			//{
			//	InitializeTwitter();
			//}
		}

		public Visibility BusyVisibility
		{
			get { return _busyVisibility; }
			set { SetValue(value, ref _busyVisibility); }
		}

		public string CallbackUrl
		{
			get
			{
				return _callbackUrl;
			}

			set
			{
				SetValue(value, ref _callbackUrl);
			}
		}

		public string ConsumerKey
		{
			get
			{
				return _consumerKey;
			}

			set
			{
				SetValue(value, ref _consumerKey);
			}
		}

		public string ConsumerSecrect
		{
			get
			{
				return _consumerSecrect;
			}

			set
			{
				SetValue(value, ref _consumerSecrect);
			}
		}

		private bool IsBusy
		{
			get { return IsVisible(BusyVisibility); }
			set { BusyVisibility = ToVisibility(value); }
		}

		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				SetValue(value, ref _message);
			}
		}

		public Brush MessageBrush
		{
			get { return _messageBrush; }
			set
			{
				SetValue(value, ref _messageBrush);
			}
		}

		public Visibility MessageVisibility
		{
			get { return _messageVisibility; }
			set { SetValue(value, ref _messageVisibility); }
		}

		public override string PageTitle => "Credentials";

		public Uri UserImage
		{
			get
			{
				return _userImage;
			}
			set
			{
				SetValue(value, ref _userImage);
			}
		}

		public string UserScreenName
		{
			get { return _userScreenName; }
			set { SetValue(value, ref _userScreenName); }
		}

		//----==== COMMANDS ====-------------------------------------------------------------------

		public ICommand ConnectCommand { get; private set; }

		//----==== PRIVATE ====-------------------------------------------------------------------

		private bool CanConnect()
		{
			return !string.IsNullOrEmpty(CallbackUrl) && !string.IsNullOrEmpty(ConsumerKey) && !string.IsNullOrEmpty(ConsumerSecrect);
		}

		private void Connect()
		{
			try
			{
				var twitterAuth = new TwitterOAuthTokens
				{
					//AccessToken = _oauthCredentials.AccessToken,
					//AccessTokenSecret = _oauthCredentials.AccessTokenSecret,
					CallbackUri = CallbackUrl,
					ConsumerKey = ConsumerKey,
					ConsumerSecret = ConsumerSecrect
				};

				TwitterService.Instance.Initialize(ConsumerKey, ConsumerSecrect, CallbackUrl);
				//TwitterService.Instance.Initialize(twitterAuth);

				TwitterService.Instance.Logout();
			}
			catch (Exception ex)
			{
				// GULP - In order to logout you have to initialize, seems like a chicken/egg problem
			}

			InitializeTwitter();
		}

		private async void InitializeTwitter()
		{
			try
			{
				IsBusy = true;

				if (!NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
				{
					Message = "No Internet";

					return;
				}

				Message = "Logging into Twitter...";

				var twitterAuth = new TwitterOAuthTokens
				{
					//AccessToken = _oauthCredentials.AccessToken,
					//AccessTokenSecret = _oauthCredentials.AccessTokenSecret,
					CallbackUri = CallbackUrl,
					ConsumerKey = ConsumerKey,
					ConsumerSecret = ConsumerSecrect
				};
				//TwitterService.Instance.Initialize(twitterAuth);

				TwitterService.Instance.Initialize(ConsumerKey, ConsumerSecrect, CallbackUrl);

				// Login to Twitter
				if (!await TwitterService.Instance.LoginAsync())
				{
					Message = "Logon Failed.";

					return;
				}

				var user = await TwitterService.Instance.GetUserAsync();

				if (user != null)
				{
					UserScreenName = user.ScreenName;
					UserImage = new Uri(user.ProfileImageUrl);

					Message = $"Logged in as {user.ScreenName}";
				}
			}
			catch (TwitterException tex)
			{
				if (tex.Errors?.Errors?.Length > 0 && tex.Errors.Errors[0].Code == 89)
				{
				}

				Message = $"Failed to logon: {tex.Message}";
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}