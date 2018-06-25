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

#### SociallyAwesomated.Tests (UW)
All of the unit tests for the UW projects.

#### TODO
Figure out what hashtags are trending the best to include them in a tweet.  Start with movie titles.

#### References
https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/navigationview