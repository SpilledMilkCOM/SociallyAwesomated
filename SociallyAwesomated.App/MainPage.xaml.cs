using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SociallyAwesomated.App
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		private readonly MenuModel _viewMap;

		public MainPage()
		{
			this.InitializeComponent();

			_viewMap = new MenuModel();
		}

		private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
		{
			On_BackRequested();
			args.Handled = true;
		}

		private void NavigateToView(string viewName)
		{
			var view = _viewMap.Map(viewName);

			if (view != null)
			{
				uxContent.Navigate(view);
			}
		}

		private bool On_BackRequested()
		{
			bool navigated = false;

			// don't go back if the nav pane is overlayed
			if (uxNavView.IsPaneOpen
				&& (uxNavView.DisplayMode == NavigationViewDisplayMode.Compact
					|| uxNavView.DisplayMode == NavigationViewDisplayMode.Minimal))
			{
				return false;
			}
			else
			{
				if (uxContent.CanGoBack)
				{
					uxContent.GoBack();
					navigated = true;
				}
			}
			return navigated;
		}

		// ----==== CONTROL EVENTS ====----------------------------------------------

		private void uxContent_Navigated(object sender, NavigationEventArgs e)
		{
			//uxNavView.IsBackEnabled = uxContent.CanGoBack;

			if (uxContent.SourcePageType == _viewMap.Map(MenuModel.SETTINGS))
			{
				// SettingsItem is not part of NavView.MenuItems, and doesn't have a Tag
				uxNavView.SelectedItem = (NavigationViewItem)uxNavView.SettingsItem;
			}
			else
			{
				var tag = _viewMap.Map(e.SourcePageType);

				uxNavView.SelectedItem = uxNavView.MenuItems
					.OfType<NavigationViewItem>()
					.First(n => n.Tag.Equals(tag));
			}
		}

		private void uxNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			if (args != null)
			{
				string navigateTo = null;

				if (args.IsSettingsInvoked)
				{
					navigateTo = MenuModel.SETTINGS;
				}
				else
				{
					var invokedItem = args.InvokedItem as NavigationViewItem;

					navigateTo = invokedItem?.Tag as string;
				}

				if (navigateTo != null)
				{
					NavigateToView(navigateTo);
				}
			}
		}

		private void uxNavView_Loaded(object sender, RoutedEventArgs e)
		{
			// NavView doesn't load any page by default: you need to specify it
			NavigateToView(MenuModel.HOME);

			// add keyboard accelerators for backwards navigation

			//KeyboardAccelerator goBack = new KeyboardAccelerator();

			//goBack.Key = VirtualKey.GoBack;
			//goBack.Invoked += BackInvoked;

			//KeyboardAccelerator altLeft = new KeyboardAccelerator();

			//altLeft.Key = VirtualKey.Left;
			//altLeft.Invoked += BackInvoked;

			//KeyboardAccelerators.Add(goBack);
			//KeyboardAccelerators.Add(altLeft);

			//// ALT routes here

			//altLeft.Modifiers = VirtualKeyModifiers.Menu;
		}

		private void uxNavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
		{
		}
	}
}