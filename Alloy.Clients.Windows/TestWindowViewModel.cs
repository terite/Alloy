/*
 * Copyright © Eric Maupin 2012
 * This file is part of Alloy.
 *
 * Alloy is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Alloy is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Alloy.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Net;
using System.Windows.Input;
using Alloy.Windows;
using Tempest.Providers.Network;

namespace Alloy.Clients.Windows
{
	internal class TestWindowViewModel
		: ViewModelBase
	{
		public TestWindowViewModel()
		{
			this.startServer = new DelegatedCommand (StartServerHandler);
			this.joinServer = new DelegatedCommand<string> (JoinServerHandler, s => !String.IsNullOrEmpty (s));
		}

		public ICommand StartServer
		{
			get { return this.startServer; }
		}

		public ICommand JoinServer
		{
			get { return this.joinServer; }
		}

		private readonly DelegatedCommand startServer;
		private readonly DelegatedCommand<string> joinServer;

		private AlloyServer server;
		private AlloyClient client;

		private void StartServerHandler (object state)
		{
			this.server = new AlloyServer (new WindowsMachine());
			this.server.AddConnectionProvider (new NetworkConnectionProvider (
				AlloyProtocol.Instance, new IPEndPoint (IPAddress.Any, 42424), 100));

			this.server.SetPositions(new []
			{
				new PositionedScreen ("vengeance", 1440, 900)
				{
					AlloyWidth = 1440,
					AlloyHeight = 900,
					AlloyX = 1920,
					AlloyY = 0
				}, 
			});

			this.server.Start();
		}

		private void JoinServerHandler (string hostname)
		{
			this.client = new AlloyClient (new WindowsMachine());
			this.client.ConnectAsync (new DnsEndPoint (hostname, 42424));
		}
	}
}