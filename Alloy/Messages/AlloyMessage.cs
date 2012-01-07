using Tempest;

namespace Alloy.Messages
{
	public enum AlloyMessageType
		: ushort
	{
		Connect = 1,
		ConnectResult = 2,
		ScreenChanged = 3,
		MouseEvent = 4,
		KeyboardEvent = 5
	}

	public abstract class AlloyMessage
		: Message
	{
		protected AlloyMessage (AlloyMessageType messageType)
			: base (AlloyProtocol.Instance, (ushort)messageType)
		{
		}
	}
}