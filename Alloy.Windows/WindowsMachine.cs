using System;
using System.Threading;

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
			int result = Interop.SetWindowsHookEx (Interop.WH_KEYBOARD, KeyboardHook, (IntPtr) 0, AppDomain.GetCurrentThreadId());
		}

		public void StopListening()
		{
			Interop.UnhookWindowsHookEx (Interop.WH_KEYBOARD);
		}

		public void SetCursorVisibility (bool visible)
		{
			throw new NotImplementedException();
		}

		public void InvokeMouseEvent(MouseEvent ev)
		{
			throw new NotImplementedException();
		}

		public void InvokeKeyboardEvent(KeyboardEvent ev)
		{
			throw new NotImplementedException();
		}

		private int KeyboardHook (int nCode, int wParam, int lParam)
		{
			if (nCode < 0)
				return nCode;

			bool stop = false;

			var kev = KeyboardEvent;
			if (kev != null)
			{
				var args = new KeyboardEventArgs (new KeyboardEvent (KeyboardEventType.Up, KeyModifiers.None, KeyCode.A, 1));
				kev (this, args);
				stop = args.Handled;
			}

			return (stop) ? -1 : Interop.CallNextHookEx (Interop.WH_KEYBOARD, nCode, wParam, lParam);
		}
	}
}