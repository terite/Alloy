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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Alloy.Messages;
using Cadenza.Collections;
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
			this.machine.MouseEvent += OnMouseEvent;
			this.machine.KeyboardEvent += OnKeyboardEvent;
			this.machine.ScreenChanged += OnMachineScreenChanged;
			
			this.centerPosition = new Position ((short)(this.machine.Screen.Height / 2), (short)(this.machine.Screen.Width / 2));

			this.RegisterMessageHandler<ConnectMessage> (OnConnectMessage);
			this.RegisterMessageHandler<MouseEventMessage> (OnMouseEventMessage);
			this.RegisterMessageHandler<KeyboardEventMessage> (OnKeyboardEventMessage);
			this.RegisterMessageHandler<ScreenChangedMessage> (OnScreenChangedMessage);

			this.manager = new ScreenManager (new PositionedScreen (hostMachine.Screen));

			this.manager.ActiveScreenChanged += OnActiveScreenChanged;
		}

		public event EventHandler<ScreenEventArgs> ScreenJoined;
		
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

		public int Width
		{
			get { return this.manager.Width; }
		}

		public override void Start()
		{
			base.Start();

			this.machine.StartListening();
		}

		/// <summary>
		/// Updates existing and future screen positions.
		/// </summary>
		/// <param name="positionedScreens">Screen positions</param>
		/// <remarks>
		/// Use this method to set connected screen positions, as well as
		/// restoring saved positions for screens that have not yet connected.
		/// </remarks>
		public void SetPositions (IEnumerable<PositionedScreen> positionedScreens)
		{
			if (positionedScreens == null)
				throw new ArgumentNullException ("positionedScreens");

			manager.UpdateScreens (positionedScreens);
		}

		private readonly ScreenManager manager;
		private readonly IMachine machine;
		private readonly BidirectionalDictionary<IConnection, Screen> screens = new BidirectionalDictionary<IConnection, Screen>();

		private void OnKeyboardEvent (object sender, KeyboardEventArgs e)
		{
			IConnection connection;
			if (!TryGetScreenConnection (this.manager.ActiveScreen, out connection))
				return;
			
			e.Handled = true;
			connection.Send (new KeyboardEventMessage { Event = e.Event });
		}

		private int lastx, lasty;

		private Position centerPosition;
		private void OnMouseEvent (object sender, MouseEventArgs e)
		{
			int dx = e.Event.Position.X - this.lastx;
			int dy = e.Event.Position.Y - this.lasty;
			this.manager.UpdateMouse (dx, dy);

			IConnection connection;
			if (TryGetScreenConnection (this.manager.ActiveScreen, out connection))
			{
				this.machine.InvokeMouseEvent (new MouseEvent (MouseEventType.Move, this.centerPosition));

				this.lastx = this.centerPosition.X;
				this.lasty = this.centerPosition.Y;

				connection.Send (new MouseEventMessage
				{
					Event = new MouseEvent (e.Event.Type, new Position ((short)dx, (short)dy))
				});
			}
			else
			{
				this.lastx = e.Event.Position.X;
				this.lasty = e.Event.Position.Y;
			}
		}

		private bool TryGetScreenConnection (Screen screen, out IConnection connection)
		{
			lock (this.screens)
				return this.screens.TryGetKey (screen, out connection);
		}

		private void OnMachineScreenChanged (object sender, EventArgs eventArgs)
		{
			this.centerPosition = new Position ((short)(this.machine.Screen.Height / 2), (short)(this.machine.Screen.Width / 2));
		}

		private void SetActive (bool isActive)
		{
			this.machine.SetCursorVisibility (isActive);
		}
		
		private void OnActiveScreenChanged (object sender, ActiveScreenChangedEventArgs e)
		{
			IConnection connection;
			if (TryGetScreenConnection (e.OldScreen, out connection))
				connection.Send (new MachineStateMessage { IsActive = false });
			else
				SetActive (false);

			if (TryGetScreenConnection (this.manager.ActiveScreen, out connection))
				connection.Send (new MachineStateMessage { IsActive = true });
			else
				SetActive (true);
		}

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
			bool removed;
			lock (this.screens)
			{
				removed = this.screens.Remove (e.Connection);
				this.screens[e.Connection] = e.Message.Screen;
			}

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