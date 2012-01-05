using System;
using Tempest;

namespace Alloy
{
	public class KeyboardEvent
	{
	}

	public class KeyboardEventSerializer
		: ISerializer<KeyboardEvent>
	{
		public static readonly KeyboardEventSerializer Instance = new KeyboardEventSerializer();

		public void Serialize (ISerializationContext context, IValueWriter writer, KeyboardEvent element)
		{
			throw new NotImplementedException();
		}

		public KeyboardEvent Deserialize (ISerializationContext context, IValueReader reader)
		{
			throw new NotImplementedException();
		}
	}
}