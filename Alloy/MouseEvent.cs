using System;
using Tempest;

namespace Alloy
{
	public struct Position
	{
		public Position (short x, short y)
		{
			X = x;
			Y = y;
		}

		public short X;
		public short Y;
	}

	public enum MouseEventType
		: byte
	{
		LeftDown = 1,
		LeftUp = 2,
		RightDown = 3,
		RightUp = 4,
		Move = 5
	}

	public class MouseEvent
	{
		public MouseEvent (MouseEventType type, Position position)
		{
			Type = type;
			Position = position;
		}

		public MouseEventType Type
		{
			get;
			private set;
		}

		public Position Position
		{
			get;
			private set;
		}
	}

	public class MouseEventSerializer
		: ISerializer<MouseEvent>
	{
		public static readonly MouseEventSerializer Instance = new MouseEventSerializer();

		public void Serialize (ISerializationContext context, IValueWriter writer, MouseEvent element)
		{
			writer.WriteByte ((byte) element.Type);
			writer.WriteInt16 (element.Position.X);
			writer.WriteInt16 (element.Position.Y);
		}

		public MouseEvent Deserialize (ISerializationContext context, IValueReader reader)
		{
			MouseEventType type = (MouseEventType) reader.ReadByte();
			short x = reader.ReadInt16();
			short y = reader.ReadInt16();

			return new MouseEvent (type, new Position (x, y));
		}
	}
}