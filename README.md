# Socially Awesomated
A project to automate social networks.  These projects will use the Universal Windows Platform (UWP)
and the <a href="https://github.com/Microsoft/UWPCommunityToolkit">UWP Community Toolkit</a>.

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