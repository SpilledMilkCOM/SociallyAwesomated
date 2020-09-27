using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SM.Common;
using SM.Common.Interfaces;
using SM.Common.REST;
using SM.Common.REST.Interfaces;
using SM.Common.Serialization;
using SM.Interfaces;
using SM.OAuth2;

namespace SM.Test.LinkedIn
{
	[TestClass]
	//[DeploymentItem("OAuth.secret.json")]			Not really being "deployed" to the bin folder
	public class AuthenticationTests
	{
		private static IServiceCollection _iocContainer = null;

		[ClassInitialize]
		public static void InitializeBeforeAllTests(TestContext context)
		{
			// https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollection?view=dotnet-plat-ext-3.1

			ServiceCollection serviceCollection = new ServiceCollection();

			serviceCollection.AddTransient<IConfigurationLoader, ConfigurationLoaderJSON>();

			var serviceProvider = serviceCollection.BuildServiceProvider();
			var loader = serviceProvider.GetService<IConfigurationLoader>();
			var credentials = loader.Load<OAuthCredentials>("..\\..\\..\\OAuth.secret.json");

			serviceCollection.AddTransient<IRestClientOptions, RestClientOptions>(serviceProvider => new RestClientOptions(credentials.TokenUrl));
			serviceCollection.AddSingleton<IRestClient, RestClient>();
			serviceCollection.AddSingleton<ISerializationUtility, JsonSerializationUtility>();

			serviceCollection.AddTransient<IAuthenticator, OAuth2Authenticator>(serviceProvider =>
					new OAuth2Authenticator(serviceProvider.GetService<IRestClient>()
											, credentials
											, serviceProvider.GetService<ISerializationUtility>()));

			_iocContainer = serviceCollection;
		}

		[TestMethod, TestCategory("Integration")]
		public void Authenticate()
		{
			var test = ConstructTestObject();

			test.Authenticate();
		}

		private IAuthenticator ConstructTestObject()
		{
			return _iocContainer.BuildServiceProvider().GetService<IAuthenticator>();
		}
	}
}
