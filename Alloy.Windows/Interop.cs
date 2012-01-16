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

	[StructLayout (LayoutKind.Sequential)]
	internal struct KEYBDINPUT
	{
		public ushort wVk;
		public ushort wScan;
		public uint dwFlags;
		public uint time;
		public IntPtr dwExtraInfo;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct MOUSEINPUT
	{
		int dx;
		int dy;
		int mouseData;
		int dwFlags;
		int time;
		IntPtr dwExtraInfo;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct HARDWAREINPUT
	{
		public int uMsg;
		public short wParamL;
		public short wParamH;
	}

	[StructLayout (LayoutKind.Explicit)]
	internal struct InputUnion
	{
		[FieldOffset(0)] public MOUSEINPUT mi;
		[FieldOffset(0)] public KEYBDINPUT ki;
		[FieldOffset(0)] public HARDWAREINPUT hi;
	}

	internal struct INPUT
	{
		public int type;
		public InputUnion u;
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

		[DllImport ("user32.dll")]
		public static extern uint SendInput (uint nInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] pInputs, Int32 cbSize);

		[DllImport ("user32.dll")]
		public static extern extern uint MapVirtualKey (uint uCode, VirtualMapType uMapType);
	}

	internal enum VirtualMapType
		: uint
	{
		VirtualKeyToScanCode = 0,
		ScanCodeToVirtualKey = 1,
		VirtualKeyToChar = 2,
		ScanCodeToVirtualKeyEx = 3,
	}
}