using System;

namespace SM.Common.Interfaces
{
	public interface IMenuModel
	{
		string SettingsMap { get; }

		Type Map(string name);

		string Map(Type type);
	}
}
