using System;
using System.Net;
using System.Threading.Tasks;
using Alloy.Messages;
using Tempest;

namespace Alloy
{
	public sealed class AlloyClient
		: ClientBase
	{
		public AlloyClient (IMachine machine, IClientConnection clientConnection)
			: base (clientConnection, MessageTypes.Reliable)
		{
			if (machine == null)
				throw new ArgumentNullException ("machine");

			this.machine = machine;
			this.machine.ScreenChanged += OnMachineScreensChanged;
		}

		public Task<ConnectionResult> ConnectAsync (EndPoint endPoint, string password)
		{
			return ConnectAsync (endPoint)
				.ContinueWith (t =>
				{
					if (t.Result == ConnectionResult.Success)
					{
						return Connection.SendFor<ConnectResultMessage> (new ConnectMessage { Password = password })
							.ContinueWith (ct =>
							{
								switch (ct.Result.Result)
								{
									case ConnectResult.Success:
										return ConnectionResult.Success;
									case ConnectResult.FailedPassword:
										return ConnectionResult.FailedHandshake;
									default:
										return ConnectionResult.FailedUnknown;
								}
							}).Result;
					}

					return t.Result;
				});
		}
		
		private readonly IMachine machine;

		private void OnMachineScreensChanged (object sender, EventArgs e)
		{
			ChangeScreens();
		}

		private void ChangeScreens()
		{
			this.connection.Send (new ScreenChangedMessage (this.machine.Screen));
		}
	}
}