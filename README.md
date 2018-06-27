# Socially Awesomated
A project to automate social networks.  These projects will use the Universal Windows Platform (UWP)
and the [UWP Community Toolkit](https://github.com/Microsoft/UWPCommunityToolkit").

#### SM.UPW.Common (UW)
Contains all the common stuff that COULD be in a NuGet package, but I'm too lazy to put it in there.  "Developers are LAZY!" - YES, and I'll say it again.
* Configuration (OAuth)
* MVVM (ModelBase, ViewModelBase, RelayCommand)
* Cache Utility
* View Utility

#### SociallyAwesomated (UW)
The class library that wraps the rules of social networking.

#### SociallyAwesomated.App (UW)
The UWP app (GUI) to drive the automation.

*Use Segoe MDL2 Assets font for icons*

#### SociallyAwesomated.Tests (UW)
All of the unit tests for the UW projects.

#### TODO
* Test Community Toolkit for Followers
* Figure out what hashtags are trending the best to include them in a tweet.  Start with movie titles.

#### Lessons Learned

* How to get the NavigationView up and running.
* The "views" (Page's) **must** have code-behind that includes the `InitializeComponent()` in the constructor *(at the very least)* otherwise you will see nothing and get no errors when using `Frame.Navigate()`. Creating a "blank" view isn't enough.

#### References
* [Navigation view](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/navigationview)
* [Using the NavigationView in your UWP applications](https://blogs.msdn.microsoft.com/appconsult/2018/05/06/using-the-navigationview-in-your-uwp-applications/)
