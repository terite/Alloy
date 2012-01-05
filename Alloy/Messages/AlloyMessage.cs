﻿using Tempest;

namespace Alloy.Messages
{
	public enum AlloyMessageType
		: ushort
	{
		Login = 1,
		ScreenChanged = 3,
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