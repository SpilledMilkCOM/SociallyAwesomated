using SM.Common.Interfaces;

namespace SM.Common
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