namespace SM.Common.Interfaces
{
	public interface IConfigurationLoader
	{
		TType Load<TType>(string fileName) where TType : IConfiguration;
	}
}