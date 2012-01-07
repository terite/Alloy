using Tempest;

namespace Alloy.Messages
{
	public class KeyboardEventMessage
		: AlloyMessage
	{
		public KeyboardEventMessage()
			: base (AlloyMessageType.KeyboardEvent)
		{
		}

		public KeyboardEvent Event
		{
			get;
			set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			KeyboardEventSerializer.Instance.Serialize (context, writer, Event);
		}

		public override void ReadPayload (ISerializationContext context, IValueReader reader)
		{
			Event = KeyboardEventSerializer.Instance.Deserialize (context, reader);
		}
	}
}