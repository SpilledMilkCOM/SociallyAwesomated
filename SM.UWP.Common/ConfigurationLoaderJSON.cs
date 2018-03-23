using SM.UWP.Common.Interfaces;
using System.IO;
using SM.Common.Serialization;

namespace SM.UWP.Common
{
	public class ConfigurationLoaderJSON : IConfigurationLoader
	{
		/// <summary>
		/// Load the JSON configuration file and return the configuration type.
		/// </summary>
		/// <typeparam name="TType">The name of the configuration type.</typeparam>
		/// <param name="fileName">The full path to the JSON file.</param>
		/// <returns></returns>
		public TType Load<TType>(string fileName)
			where TType : IConfiguration
		{
			TType result = default(TType);

			using (var streamReader = File.OpenText(fileName))
			{
				var jsonUtil = new JsonSerializationUtility();

				result = jsonUtil.Deserialize<TType>(streamReader);
			}

			return result;
		}
	}
}