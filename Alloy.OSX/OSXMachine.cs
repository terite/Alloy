/*
 * Copyright © David Stensland 2012
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
using System.Drawing;
using MonoMac.AppKit;
using MonoMac.CoreGraphics;
using MonoMac.CoreFoundation;
using System.Threading;

namespace Alloy.OSX
{
	public class OSXMachine
		: IMachine
	{
		public event EventHandler<MouseEventArgs> MouseEvent;
		public event EventHandler<KeyboardEventArgs> KeyboardEvent;
		public event EventHandler ScreenChanged;
		
		private CGEventSource Source;
		private NSApplication Application;
		
		public OSXMachine ()
		{
			// TODO: I don't think this works exactly as intended but
			// it does get rid of an occasional crash.
			var blocker = new ManualResetEvent(false);
			Application = new NSApplication();
			Application.DidFinishLaunching += (sender, e) => {
				blocker.Set();
				Console.WriteLine("App did finish launching");
			};
			NSApplication.Init();
			blocker.WaitOne(500);
			Source = new CGEventSource(CGEventSourceStateId.HIDSystemState);
		}
		
		public Screen Screen
		{
			get {
				// TODO: Calculate a "box" around all screens.
				// right now it just uses the main screen.
				var f = NSScreen.Screens[0].Frame;
				
				//return new Screen((int)f.Height, (int)f.Width);
				return new Screen(Environment.MachineName, (int)f.Height, (int)f.Width);
			}
		}

		public void StartListening()
		{
			throw new NotImplementedException();
		}

		public void StopListening()
		{
			throw new NotImplementedException();
		}

		public void SetCursorVisibility (bool visible)
		{
			CGSConnection.DefaultConnection().SetProperty(
				CGSConnection.DefaultConnection(),
				"SetsCursorInBackground",
				CFBoolean.True
			);

			NSCursor.SetHiddenUntilMouseMoves(!visible);
		}

		/// <exception cref='NotImplementedException'>
		/// Is thrown for some MouseEventTypes
		/// </exception>
		public void InvokeMouseEvent (MouseEvent ev)
		{
			// TODO: Special logic for detecting double click.
			CGEventType type;
			CGMouseButton button = CGMouseButton.Left;
			
			switch (ev.Type) {
				case MouseEventType.Move:
					type = CGEventType.MouseMoved;
					break;
			
				case MouseEventType.LeftDown:
					type = CGEventType.LeftMouseDown;
					break;
				
				case MouseEventType.LeftUp:
					type = CGEventType.LeftMouseUp;
					break;
				
				case MouseEventType.RightDown:
					type = CGEventType.RightMouseDown;
					button = CGMouseButton.Right;
					break;
				
				case MouseEventType.RightUp:
					type = CGEventType.RightMouseUp;
					button = CGMouseButton.Right;
					break;
				default:
					throw new NotImplementedException();
			}
			
			PointF point = new PointF {
				X = ev.Position.X,
				Y = ev.Position.Y
			};
			
			var cge = CGEvent.CreateMouseEvent(Source, type, point, button);
			cge.Post(CGEventTapLocation.HIDEventTap);
		}

		public void InvokeKeyboardEvent (KeyboardEvent ev)
		{
			throw new NotImplementedException();
		}
	}
}
