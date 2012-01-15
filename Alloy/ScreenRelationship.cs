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

		public short StartPercent
		{
			get;
			set;
		}

		public short EndPercent
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
			writer.WriteByte ((byte) element.Direction);
			writer.WriteString (element.Origin);
			writer.WriteString (element.Destination);
			writer.WriteInt16 (element.StartPercent);
			writer.WriteInt16 (element.EndPercent);
		}

		public ScreenRelationship Deserialize (ISerializationContext context, IValueReader reader)
		{
			ScreenRelationship relationship = new ScreenRelationship();
			relationship.Direction = (Direction) reader.ReadByte();
			relationship.Origin = reader.ReadString();
			relationship.Destination = reader.ReadString();
			relationship.StartPercent = reader.ReadInt16();
			relationship.EndPercent = reader.ReadInt16();

			return relationship;
		}
	}
}
