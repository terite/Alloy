using Tempest;

namespace Alloy.Messages
{
	public enum ConnectResult
		: byte
	{
		FailedUnknown = 0,
		Success = 1,
		FailedPassword = 2,
	}

	public class ConnectResultMessage
		: AlloyMessage
	{
		public ConnectResultMessage()
			: base (AlloyMessageType.ConnectResult)
		{
		}

		public ConnectResult Result
		{
			get;
			set;
		}

		public bool KeyboardEncryption
		{
			get;
			set;
		}

		public bool MouseEncryption
		{
			get;
			set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			writer.WriteByte ((byte) Result);
			writer.WriteBool (KeyboardEncryption);
			writer.WriteBool (MouseEncryption);
		}

		public override void ReadPayload(ISerializationContext context, IValueReader reader)
		{
			Result = (ConnectResult)reader.ReadByte();
			KeyboardEncryption = reader.ReadBool();
			MouseEncryption = reader.ReadBool();
		}
	}
}