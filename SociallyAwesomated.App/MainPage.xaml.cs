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
        public MainPage()
        {
            this.InitializeComponent();
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

		private void uxContent_Navigated(object sender, NavigationEventArgs e)
		{
		}

		private void uxNavView_Loaded(object sender, RoutedEventArgs e)
		{
			uxContent.Navigated += uxContent_Navigated;

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

		private void uxNavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
		}
	}
}