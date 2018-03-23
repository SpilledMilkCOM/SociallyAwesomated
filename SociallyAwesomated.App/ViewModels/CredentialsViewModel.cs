using Microsoft.Toolkit.Uwp.Services.Twitter;
using SM.Common;
using SM.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SM.Twitter.Automated.ViewModels
{
	public class CredentialsViewModel : ViewModelBase
	{
		private const string ATTAG = "@";

		private Visibility _busyVisibility;
		private string _callbackUrl;
		private string _consumerKey;
		private string _consumerSecrect;
		private string _filter;
		private string _message;
		private Brush _messageBrush;
		private Visibility _messageVisibility;
		private ObservableCollection<Tweet> _tweets;
		private Uri _userImage;
		private string _userScreenName;

		public CredentialsViewModel()
		{
			var test = new OAuthCredentialLoader();

			// TODO: File name from configuration.

			var oathConfiguration = test.Load("OAuth.default.secret.json");        // File is marked as "content" - Copy if newer.

			CallbackUrl = oathConfiguration.CallbackUri;
			ConsumerKey = oathConfiguration.ConsumerKey;
			ConsumerSecrect = oathConfiguration.ConsumerSecret;
			Tweets = new ObservableCollection<Tweet>();

			ConnectCommand = new RelayCommand(Connect, CanConnect);
			FindCommand = new RelayCommand(Find, CanFind);

			BusyVisibility = ToVisibility(false);

			if (!IsDesignModeEnabled)
			{
				InitializeTwitter();
			}
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

		public string Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				if (SetValue(value, ref _filter))
				{
					((RelayCommand)FindCommand).RaiseCanExecuteChanged();
				}
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

		public override string PageTitle => "Twitter Automation";

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

		public ObservableCollection<Tweet> Tweets
		{
			get { return _tweets; }
			set { SetValue(value, ref _tweets); }
		}

		//----==== COMMANDS ====-------------------------------------------------------------------

		public ICommand ConnectCommand { get; private set; }

		public ICommand FindCommand { get; private set; }

		public void ShowMessage(string message)
		{
			//if (_dispatcher != null)
			//{
			//	_dispatcher.BeginInvoke(() => MessageBox.Show(message));
			//}
		}

		//----==== PRIVATE ====-------------------------------------------------------------------

		private bool CanConnect()
		{
			return !string.IsNullOrEmpty(CallbackUrl) && !string.IsNullOrEmpty(ConsumerKey) && !string.IsNullOrEmpty(ConsumerSecrect);
		}

		private bool CanFind()
		{
			return !string.IsNullOrEmpty(Filter) && Filter.Length > 3;
		}

		private void Connect()
		{
			TwitterService.Instance.Logout();

			InitializeTwitter();
		}

		private void Find()
		{
			FetchTweets(Filter);
		}

		private string GetAccount(string account)
		{
			string result = null;

			if (account != null && account.IndexOf(ATTAG) == 0)
			{
				result = account.Replace(ATTAG, string.Empty);
			}

			return result;
		}

		private async void InitializeTwitter()
		{
			try
			{
				IsBusy = true;

				Message = "Logging into Twitter...";

				TwitterService.Instance.Initialize(ConsumerKey, ConsumerSecrect, CallbackUrl);

				var user = await TwitterService.Instance.GetUserAsync();

				UserScreenName = user.ScreenName;
				UserImage = new Uri(user.ProfileImageUrl);

				Message = $"Logged in as {user.ScreenName}";

				FetchTweets($"@{user.ScreenName}");
			}
			catch (Exception ex)
			{
				ShowMessage(ex.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private async void FetchTweets(string fetchText)
		{
			IEnumerable<Tweet> timeLine = null;

			try
			{
				IsBusy = true;

				var filter = GetAccount(fetchText);

				if (filter != null)
				{
					timeLine = await TwitterService.Instance.GetUserTimeLineAsync(filter, 10);
				}
				else
				{
					timeLine = await TwitterService.Instance.SearchAsync(fetchText, 10);
				}

				Tweets.Clear();

				foreach (var tweet in timeLine)
				{
					Tweets.Add(tweet);
				}
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}