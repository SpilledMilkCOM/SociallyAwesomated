using System;
using System.Linq;
using Windows.System;
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

		public void uxContent_Navigated(object sender, NavigationEventArgs e)
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

		public void uxNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			if (args != null)
			{
				Type navigateTo = null;

				if (args.IsSettingsInvoked)
				{
					navigateTo = _viewMap.Map(MenuModel.SETTINGS);
				}
				else
				{
					var invokedItem = args.InvokedItem as NavigationViewItem;

					navigateTo = _viewMap.Map(invokedItem?.Tag as string);
				}

				if (navigateTo != null)
				{
					uxContent.Navigate(navigateTo);
				}
			}
		}

		public void uxNavView_Loaded(object sender, RoutedEventArgs e)
		{
			uxContent.Navigated += uxContent_Navigated;

			// NavView doesn't load any page by default: you need to specify it
			uxContent.Navigate(_viewMap.Map(MenuModel.SETTINGS));

			// add keyboard accelerators for backwards navigation

			KeyboardAccelerator GoBack = new KeyboardAccelerator();

			GoBack.Key = VirtualKey.GoBack;
			GoBack.Invoked += BackInvoked;

			KeyboardAccelerator AltLeft = new KeyboardAccelerator();
			AltLeft.Key = VirtualKey.Left;
			AltLeft.Invoked += BackInvoked;
			this.KeyboardAccelerators.Add(GoBack);
			this.KeyboardAccelerators.Add(AltLeft);

			// ALT routes here

			AltLeft.Modifiers = VirtualKeyModifiers.Menu;
		}
	}
}