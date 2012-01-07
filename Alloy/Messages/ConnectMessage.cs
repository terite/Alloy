using System;
using Tempest;

namespace Alloy.Messages
{
	public class ConnectMessage
		: AlloyMessage
	{
		public ConnectMessage()
			: base (AlloyMessageType.Connect)
		{
		}

		public override bool Encrypted
		{
			get { return true; }
		}

		public string Password
		{
			get;
			set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			writer.WriteString (Password ?? String.Empty);
		}

		public override void ReadPayload(ISerializationContext context, IValueReader reader)
		{
			Password = reader.ReadString();
		}
	}
}