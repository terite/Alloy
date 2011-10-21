using System;
using Alloy.Messages;
using Tempest;

namespace Alloy
{
	public sealed class AlloyClient
		: ClientBase
	{
		public AlloyClient (IClientMachine machine, IClientConnection clientConnection)
			: base (clientConnection, MessageTypes.Reliable)
		{
			if (machine == null)
				throw new ArgumentNullException ("machine");

			this.machine = machine;
			this.machine.ScreensChanged += OnMachineScreensChanged;
		}
		
		private readonly IClientMachine machine;

		private void OnMachineScreensChanged (object sender, EventArgs e)
		{
			ChangeScreens();
		}

		protected override void OnConnected (EventArgs e)
		{
			base.OnConnected(e);
			
			ChangeScreens();
		}

		private void ChangeScreens()
		{
			this.connection.Send (new ScreensChangedMessage (this.machine.Screens));
		}
	}
}