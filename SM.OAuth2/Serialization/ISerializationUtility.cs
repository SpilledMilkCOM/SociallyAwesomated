using System;
using System.IO;

namespace SM.Common.Serialization
{
	public interface ISerializationUtility
	{
		TType Deserialize<TType>(string jsonData);

		object Deserialize(string jsonData, Type type);

		object Deserialize(TextReader streamReader, Type type);

		TType Deserialize<TType>(TextReader streamReader);

		string Serialize(object obj);

		void Serialize(StreamWriter streamWriter, object obj);
	}
}