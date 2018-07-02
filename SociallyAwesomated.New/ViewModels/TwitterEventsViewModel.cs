using Microsoft.Toolkit.Uwp.Services.Twitter;
using SM.Common;
using SM.Common.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SociallyAwesomated.App.ViewModels
{
	public class TwitterEventsViewModel : ViewModelBase
	{
		private const string ATTAG = "@";

		private Visibility _busyVisibility;
		private string _filter;
		private string _message;
		private Brush _messageBrush;
		private Visibility _messageVisibility;
		private ObservableCollection<Tweet> _tweets;

		public TwitterEventsViewModel()
		{
			Tweets = new ObservableCollection<Tweet>();

			if (!IsDesignModeEnabled)
			{
				FindCommand = new RelayCommand(Find, CanFind);

				FetchTweets(null);
			}
			else
			{
				// DESIGN DATA - HIDE this data in a region?

				Tweets.Add(new Tweet { Text = "Test tweet 1." });
				Tweets.Add(new Tweet { Text = "Test tweet 2." });
				Tweets.Add(new Tweet { Text = "Test tweet 3." });
			}
		}

		public Visibility BusyVisibility
		{
			get { return _busyVisibility; }
			set { SetValue(value, ref _busyVisibility); }
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

		public override string PageTitle => "Tweets";

		public ObservableCollection<Tweet> Tweets
		{
			get { return _tweets; }
			set { SetValue(value, ref _tweets); }
		}

		//----==== COMMANDS ====-------------------------------------------------------------------

		public ICommand FindCommand { get; private set; }

		//----==== PRIVATE ====-------------------------------------------------------------------

		private bool CanFind()
		{
			return !string.IsNullOrEmpty(Filter) && Filter.Length > 3;
		}

		private void Find()
		{
			FetchTweets(Filter);
		}

		private async void FetchTweets(string fetchText)
		{
			IEnumerable<Tweet> timeLine = null;

			try
			{
				IsBusy = true;

				var filter = GetAccount(fetchText);

				if (filter == null)
				{
					var user = await TwitterService.Instance.GetUserAsync();

					if (user!= null)
					{
						timeLine = await TwitterService.Instance.GetUserTimeLineAsync(user.ScreenName, 10);
					}
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

		private string GetAccount(string account)
		{
			string result = null;

			if (account != null && account.IndexOf(ATTAG) == 0)
			{
				result = account.Replace(ATTAG, string.Empty);
			}

			return result;
		}
	}
}