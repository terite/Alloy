using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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

	internal class Interop
	{
		[DllImport ("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport ("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		[DllImport ("user32.dll")]
		public static extern IntPtr GetShellWindow();
	}
}