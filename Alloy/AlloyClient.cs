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
			this.machine.KeyboardEvent += OnKeyboardEvent;
			this.machine.MouseEvent += OnMouseEvent;

			this.RegisterMessageHandler<MachineStateMessage> (OnMachineStateMessage);
			this.RegisterMessageHandler<MouseEventMessage> (OnMouseEventMessage);
			this.RegisterMessageHandler<KeyboardEventMessage> (OnKeyboardEventMessage);
		}

		public Task<ConnectionResult> ConnectAsync (EndPoint endPoint, string password)
		{
			this.password = password;
			return ConnectAsync (endPoint)
				.ContinueWith (t =>
				{
					if (t.Result == ConnectionResult.Success)
					{
						return Connection.SendFor<ConnectResultMessage> (new ConnectMessage { Password = password })
							.ContinueWith (ct =>
							{
								OnMachineScreensChanged (null, EventArgs.Empty);
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

		private string password;
		private readonly IMachine machine;
		private bool isActive;

		private Position lastPosition;

		private void OnMouseEvent (object sender, MouseEventArgs e)
		{
			this.lastPosition = e.Event.Position;

			if (!this.isActive)
				return;

			e.Handled = true;
			this.connection.Send (new MouseEventMessage { Event = e.Event });
		}

		private void OnKeyboardEvent (object sender, KeyboardEventArgs e)
		{
			if (!this.isActive)
				return;

			e.Handled = true;
			this.connection.Send (new KeyboardEventMessage { Event = e.Event });
		}

		private void OnKeyboardEventMessage (MessageEventArgs<KeyboardEventMessage> e)
		{
			this.machine.InvokeKeyboardEvent (e.Message.Event);
		}
		
		private void OnMouseEventMessage (MessageEventArgs<MouseEventMessage> e)
		{
			MouseEvent ev = e.Message.Event;
			if (ev.Type == MouseEventType.Move)
				ev = ApplyDeltas (ev);

			this.machine.InvokeMouseEvent (ev);
		}

		private MouseEvent ApplyDeltas (MouseEvent ev)
		{
			Position p = this.lastPosition;
			p.X += ev.Position.X;
			p.Y += ev.Position.Y;

			return new MouseEvent (ev.Type, p);
		}

		private void OnMachineStateMessage (MessageEventArgs<MachineStateMessage> e)
		{
			this.isActive = e.Message.IsActive;
			this.machine.SetCursorVisibility (e.Message.IsActive);
		}

		private void OnMachineScreensChanged (object sender, EventArgs e)
		{
			ChangeScreens();
		}

		private void ChangeScreens()
		{
			this.connection.Send (new ScreenChangedMessage { Screen = this.machine.Screen });
		}
	}
}