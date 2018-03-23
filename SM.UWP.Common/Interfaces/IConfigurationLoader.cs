namespace SM.UWP.Common.Interfaces
{
	public interface IConfigurationLoader
	{
		TType Load<TType>(string fileName) where TType : IConfiguration;
	}
}