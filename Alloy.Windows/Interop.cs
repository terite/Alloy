using System;
using System.Runtime.InteropServices;

namespace Alloy.Windows
{
	[StructLayout (LayoutKind.Sequential)]
	internal struct RECT
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}

	internal delegate int HookProc (int nCode, int wParam, int lParam);

	internal static class Interop
	{
		public const int WH_KEYBOARD = 2;
		public const int WH_MOUSE = 7;

		[DllImport ("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport ("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		[DllImport ("user32.dll")]
		public static extern IntPtr GetShellWindow();

		[DllImport ("user32.dll")]
		public static extern int CallNextHookEx (int idHook, int nCode, int wParam, int lParam);

		[DllImport ("user32.dll")]
		public static extern int SetWindowsHookEx (int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

		[DllImport ("user32.dll")]
		public static extern bool UnhookWindowsHookEx (int idHook);
	}
}