using Newtonsoft.Json;

namespace SM.Common.Serialization
{
	public class FormattedJsonSerializationUtility : JsonSerializationUtility
	{
		public FormattedJsonSerializationUtility()
			: base(new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
				PreserveReferencesHandling = PreserveReferencesHandling.All,
				TypeNameHandling = TypeNameHandling.Auto,
				Formatting = Formatting.Indented
			})
		{
		}
	}
}