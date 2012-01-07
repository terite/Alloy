using System;

namespace Alloy.Windows
{
	public class WindowsMachine
		: IMachine
	{
		public event EventHandler ScreenChanged;
		public event EventHandler<MouseEventArgs> MouseEvent;
		public event EventHandler<KeyboardEventArgs> KeyboardEvent;

		public Screen Screen
		{
			get { throw new NotImplementedException(); }
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
			throw new NotImplementedException();
		}
	}
}