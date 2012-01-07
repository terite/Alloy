using System;
using Tempest;

namespace Alloy
{
	public enum Direction
		: byte
	{
		Up = 1,
		Left = 2,
		Right = 3,
		Down = 4
	}

	public class ScreenRelationship
	{
		public Direction Direction
		{
			get;
			set;
		}

		public string Origin
		{
			get;
			set;
		}

		public string Destination
		{
			get;
			set;
		}

		public int StartPercent
		{
			get;
			set;
		}

		public int EndPercent
		{
			get;
			set;
		}
	}

	public class ScreenRelationshipSerializer
		: ISerializer<ScreenRelationship>
	{
		public void Serialize (ISerializationContext context, IValueWriter writer, ScreenRelationship element)
		{
			throw new NotImplementedException();
		}

		public ScreenRelationship Deserialize (ISerializationContext context, IValueReader reader)
		{
			throw new NotImplementedException();
		}
	}
}
