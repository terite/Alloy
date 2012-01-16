using System;
using Tempest;

namespace Alloy
{
	public enum KeyboardEventType
		: byte
	{
		Up = 1,
		Down = 2
	}

	public class KeyboardEvent
	{
		public KeyboardEvent (KeyboardEventType type, KeyModifiers modifiers, KeyCode code, int repeatCount)
		{
			if (repeatCount <= 0)
				throw new ArgumentException ("repeatCount must be greater than 0", "repeatCount");

			Type = type;
			Modifiers = modifiers;
			Code = code;
			RepeatCount = repeatCount;
		}

		public int RepeatCount
		{
			get;
			private set;
		}

		public KeyboardEventType Type
		{
			get;
			private set;
		}

		public KeyModifiers Modifiers
		{
			get;
			private set;
		}

		public KeyCode Code
		{
			get;
			private set;
		}
	}

	public class KeyboardEventSerializer
		: ISerializer<KeyboardEvent>
	{
		public static readonly KeyboardEventSerializer Instance = new KeyboardEventSerializer();

		public void Serialize (ISerializationContext context, IValueWriter writer, KeyboardEvent element)
		{
			// TODO: combine type and code into single byte
			writer.WriteByte ((byte)element.Type);
			writer.WriteUInt16 ((ushort)element.Modifiers);
			writer.WriteByte ((byte)element.Code);
		}

		public KeyboardEvent Deserialize (ISerializationContext context, IValueReader reader)
		{
			KeyboardEventType type = (KeyboardEventType) reader.ReadByte();
			KeyModifiers modifiers = (KeyModifiers) reader.ReadUInt16();
			KeyCode code = (KeyCode) reader.ReadByte();

			return new KeyboardEvent (type, modifiers, code);
		}
	}
}