using Microsoft.Toolkit.Uwp.Services.Twitter;
using SM.Common;
using SM.Common.ViewModels;
using SociallyAwesomated.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SociallyAwesomated.App.ViewModels
{
	public class TwitterFollowersViewModel : ViewModelBase
	{
		private const string ATTAG = "@";

		private Visibility _busyVisibility;
		private string _filter;
		private string _message;
		private Brush _messageBrush;
		private Visibility _messageVisibility;
		private ObservableCollection<MyTwitterUser> _followers;

		public TwitterFollowersViewModel()
		{
			Followers = new ObservableCollection<MyTwitterUser>();

			if (!IsDesignModeEnabled)
			{
				FetchFollowers();
			}
			else
			{
				// DESIGN DATA - HIDE this data in a region?

				Followers.Add(new MyTwitterUser { ScreenName = "@TestUser1" });
				Followers.Add(new MyTwitterUser { ScreenName = "@TestUser2" });
				Followers.Add(new MyTwitterUser { ScreenName = "@TestUser3" });
			}
		}

		public Visibility BusyVisibility
		{
			get { return _busyVisibility; }
			set { SetValue(value, ref _busyVisibility); }
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

		public override string PageTitle => "Followers";

		public ObservableCollection<MyTwitterUser> Followers
		{
			get { return _followers; }
			set { SetValue(value, ref _followers); }
		}

		//----==== PRIVATE ====-------------------------------------------------------------------

		private async void FetchFollowers()
		{
			try
			{
				IsBusy = true;

				var config = new TwitterDataConfig { Query = string.Empty, QueryType = TwitterQueryType.User };

				List<MyTwitterUser> users = await TwitterService.Instance.RequestAsync<MyTwitterUser>(config);

				Followers.Clear();

				foreach (var tweet in users)
				{
					Followers.Add(tweet);
				}
			}
			catch (TwitterException tex)
			{
				if (tex.Errors?.Errors?.Length > 0 && tex.Errors.Errors[0].Code == 89)
				{
				}

				Message = $"Failed obtain followers: {tex.Message}";
			}
			catch (Exception ex)
			{
				Message = $"Failed obtain followers: {ex.Message}";
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}