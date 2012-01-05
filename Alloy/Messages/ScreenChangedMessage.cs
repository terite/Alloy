using System;
using Tempest;

namespace Alloy.Messages
{
	public sealed class ScreenChangedMessage
		: AlloyMessage
	{
		public ScreenChangedMessage (Screen screen)
			: this()
		{
			if (screen == null)
				throw new ArgumentNullException ("screen");

			Screen = screen;
		}

		private ScreenChangedMessage()
			: base (AlloyMessageType.ScreenChanged)
		{
		}

		public Screen Screen
		{
			get;
			private set;
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