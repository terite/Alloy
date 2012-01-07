using Tempest;

namespace Alloy.Messages
{
	public class MouseEventMessage
		: AlloyMessage
	{
		public MouseEventMessage()
			: base (AlloyMessageType.MouseEvent)
		{
		}

		public MouseEvent Event
		{
			get;
			set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			MouseEventSerializer.Instance.Serialize (context, writer, Event);
		}

		public override void ReadPayload (ISerializationContext context, IValueReader reader)
		{
			Event = MouseEventSerializer.Instance.Deserialize (context, reader);
		}
	}
}