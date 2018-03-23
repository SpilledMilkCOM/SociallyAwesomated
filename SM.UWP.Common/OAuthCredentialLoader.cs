using SM.UWP.Common.Interfaces;

namespace SM.UWP.Common
{
	public class OAuthCredentialLoader
	{
		public IOAuthCredentials Load(string jsonFileName)
		{
			var loader = new ConfigurationLoaderJSON();

			return loader.Load<OAuthCredentials>(jsonFileName);
		}
	}
}