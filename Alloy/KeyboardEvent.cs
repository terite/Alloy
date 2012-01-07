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
		public KeyboardEvent (KeyboardEventType type, KeyModifiers modifiers, KeyCode code)
		{
			Type = type;
			Modifiers = modifiers;
			Code = code;
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
			throw new NotImplementedException();
		}

		public KeyboardEvent Deserialize (ISerializationContext context, IValueReader reader)
		{
			throw new NotImplementedException();
		}
	}
}