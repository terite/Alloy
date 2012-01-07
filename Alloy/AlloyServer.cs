using System;
using Alloy.Messages;
using Tempest;

namespace Alloy
{
	public sealed class AlloyServer
		: ServerBase
	{
		public AlloyServer (IMachine hostMachine)
			: base (MessageTypes.Reliable)
		{
			this.machine = hostMachine;

			this.RegisterMessageHandler<ConnectMessage> (OnConnectMessage);
		}

		public string ServerPassword
		{
			get;
			set;
		}

		public bool MouseEventEncryption
		{
			get;
			set;
		}

		public bool KeyboardEventEncryption
		{
			get;
			set;
		}

		private readonly IMachine machine;

		private void OnConnectMessage (MessageEventArgs<ConnectMessage> e)
		{
			if (e.Message.Password != ServerPassword)
			{
				e.Connection.SendResponse (e.Message, new ConnectResultMessage { Result = ConnectResult.FailedPassword });
				e.Connection.DisconnectAsync();
			}
			else
			{
				e.Connection.SendResponse (e.Message, new ConnectResultMessage
				{
					Result = ConnectResult.Success,
					KeyboardEncryption = KeyboardEventEncryption,
					MouseEncryption = MouseEventEncryption
				});
			}
		}
	}
}