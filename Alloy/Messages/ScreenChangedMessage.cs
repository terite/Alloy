using System;
using Tempest;

namespace Alloy.Messages
{
	public sealed class ScreenChangedMessage
		: AlloyMessage
	{
		public ScreenChangedMessage()
			: base (AlloyMessageType.ScreenChanged)
		{
		}

		public Screen Screen
		{
			get;
			set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			ScreenSerializer.Instance.Serialize (context, writer, Screen);
		}

		public override void ReadPayload (ISerializationContext context, IValueReader reader)
		{
			Screen = ScreenSerializer.Instance.Deserialize (context, reader);
		}
	}
}