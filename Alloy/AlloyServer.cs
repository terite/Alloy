﻿/*
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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
			if (hostMachine == null)
				throw new ArgumentNullException ("hostMachine");

			this.machine = hostMachine;

			this.RegisterMessageHandler<ConnectMessage> (OnConnectMessage);
			this.RegisterMessageHandler<MouseEventMessage> (OnMouseEventMessage);
			this.RegisterMessageHandler<KeyboardEventMessage> (OnKeyboardEventMessage);
			this.RegisterMessageHandler<ScreenChangedMessage> (OnScreenChangedMessage);

			Screen = new AlloyScreen (new[] { new PositionedScreen (hostMachine.Screen) });
		}

		public event EventHandler<ScreenEventArgs> ScreenJoined;

		public AlloyScreen Screen
		{
			get;
			private set;
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

		public void RestorePositions (IEnumerable<PositionedScreen> positionedScreens)
		{
			throw new NotSupportedException();
			if (positionedScreens == null)
				throw new ArgumentNullException ("positionedScreens");

			this.restore = positionedScreens.ToArray();
		}

		private PositionedScreen[] restore;
		private readonly IMachine machine;
		private readonly ConcurrentDictionary<IConnection, Screen> connectionScreens = new ConcurrentDictionary<IConnection, Screen>();

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

		private void OnKeyboardEventMessage (MessageEventArgs<KeyboardEventMessage> e)
		{
			
		}

		private void OnMouseEventMessage (MessageEventArgs<MouseEventMessage> e)
		{
			
		}

		private void OnScreenChangedMessage (MessageEventArgs<ScreenChangedMessage> e)
		{
			Screen old;
			bool removed = this.connectionScreens.TryRemove (e.Connection, out old);
			this.connectionScreens[e.Connection] = e.Message.Screen;

			if (!removed)
			{
				var joined = ScreenJoined;
				if (joined != null)
					joined (this, new ScreenEventArgs (e.Message.Screen));
			}
		}
	}

	public class ScreenEventArgs
		: EventArgs
	{
		public ScreenEventArgs (Screen screen)
		{
			if (screen == null)
				throw new ArgumentNullException ("screen");
			
			Screen = screen;
		}

		public Screen Screen
		{
			get;
			private set;
		}
	}
}