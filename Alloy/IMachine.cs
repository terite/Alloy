using System;
using System.Collections.Generic;

namespace Alloy
{
	/// <summary>
	/// Represents any machine.
	/// </summary>
	public interface IMachine
	{
		/// <summary>
		/// Raised when a mouse event occurs.
		/// </summary>
		event EventHandler<MouseEventArgs> MouseEvent;

		/// <summary>
		/// Raised when a keyboard event occurs.
		/// </summary>
		event EventHandler<KeyboardEventArgs> KeyboardEvent;

		/// <summary>
		/// Raised when the screen has changed.
		/// </summary>
		event EventHandler ScreenChanged;

		/// <summary>
		/// Gets a screen instance representing the total screen area for the machine.
		/// </summary>
		Screen Screen { get; }

		/// <summary>
		/// Begins listening for events.
		/// </summary>
		void StartListening();

		/// <summary>
		/// Stops listening for events.
		/// </summary>
		void StopListening();

		/// <summary>
		/// Set the mouse cursor visibility on the machine.
		/// </summary>
		/// <param name="visible">Whether the cursor should now be visible or not.</param>
		void SetCursorVisibility (bool visible);

		/// <summary>
		/// Invokes a mouse event on this machine.
		/// </summary>
		/// <param name="ev">The event to invoke.</param>
		/// <exception cref="ArgumentNullException"><paramref name="ev"/> is <c>null</c>.</exception>
		void InvokeMouseEvent (MouseEvent ev);

		/// <summary>
		/// Invokes a keyboard event on this machine.
		/// </summary>
		/// <param name="ev">The event to invoke.</param>
		/// <exception cref="ArgumentNullException"><paramref name="ev"/> is <c>null</c>.</exception>
		void InvokeKeyboardEvent (KeyboardEvent ev);
	}

	public class MouseEventArgs
		: EventArgs
	{
		public MouseEventArgs (MouseEvent ev)
		{
			if (ev == null)
				throw new ArgumentNullException ("ev");

			Event = ev;
		}

		public MouseEvent Event
		{
			get;
			private set;
		}
	}

	public class KeyboardEventArgs
		: EventArgs
	{
		public KeyboardEventArgs (KeyboardEvent ev)
		{
			this.Event = ev;
			if (ev == null)
				throw new ArgumentNullException ("ev");
		}

		public KeyboardEvent Event
		{
			get;
			set;
		}
	}
}
