using System;
using Tempest;

namespace Alloy
{
	public class MouseEvent
	{
	}

	public class MouseEventSerializer
		: ISerializer<MouseEvent>
	{
		public static readonly MouseEventSerializer Instance = new MouseEventSerializer();

		public void Serialize (ISerializationContext context, IValueWriter writer, MouseEvent element)
		{
			throw new NotImplementedException();
		}

		public MouseEvent Deserialize (ISerializationContext context, IValueReader reader)
		{
			throw new NotImplementedException();
		}
	}
}