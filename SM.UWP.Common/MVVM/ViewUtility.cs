using SM.Common.Interfaces;
using System;
using System.Linq;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SM.Common
{
	public static class ViewUtility
	{
		public static void ExecuteRelayCommand(RelayCommand command)
		{
			if (command.CanExecute(null))
			{
				command.Execute(null);
			}
		}

		public static bool IsThemeDark()
		{
			return Application.Current.RequestedTheme == ApplicationTheme.Dark || DesignMode.DesignModeEnabled;
		}

		public static string ParseNavigationItemInvokedEvent(NavigationView sender, NavigationViewItemInvokedEventArgs args, IMenuModel menuModel)
		{
			string result = null;

			if (args != null)
			{
				if (args.IsSettingsInvoked)
				{
					result = menuModel.SettingsMap;
				}
				else
				{
					// The Content property is being set to a string (versus it being part of the "contents" of the item node)
					// InvokedItem is a string and can be compared against the navItem (which is also a string)
					// Comparing these by strings will NOT return the second "Events" view since it will match to the first one.

					// NOTE: Using a TextBlock as the Content of the NavigationViewItem

					//var item = sender?.MenuItems.OfType<NavigationViewItem>().FirstOrDefault(navItem => navItem.Content.Equals(args.InvokedItem));

					var invokedTag = ((TextBlock)args.InvokedItem).Tag;
					var item = sender?.MenuItems.OfType<NavigationViewItem>().FirstOrDefault(navItem => ((FrameworkElement)navItem.Content).Tag.Equals(invokedTag));

					result = item?.Tag as string;
				}
			}

			return result;
		}

		internal static bool NeedsBackButton()
		{
			return true;
		}

		public static void SetBackButtonVisibility()
		{
			Frame rootFrame = Window.Current.Content as Frame;

			if (rootFrame.CanGoBack)
			{
				// Show UI in title bar if opted-in and in-app backstack is not empty.
				SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
			}
			else
			{
				// Remove the UI from the title bar if in-app back stack is empty.
				SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
			}
		}

		public static TViewModel ViewModel<TViewModel>(object dataContext)
			where TViewModel : class
		{

			TViewModel viewModel = dataContext as TViewModel;

			if (viewModel == null)
			{
				throw new ArgumentNullException("dataContext", string.Format("The DataContext was not initialized with the {0}", typeof(TViewModel).Name));
			}

			return viewModel;
		}

	}
}