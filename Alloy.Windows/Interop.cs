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

	internal enum SystemMetric
	{
		XScreen = 0,
		YScreen = 1,
		XBorder = 5,
		YBorder = 6,
		XIcon = 11,
		YIcon = 12,
		XFullscreen = 16,
		YFullscreen = 17,
		XSmallIcon = 49,
		YSmallIcon = 50,
		XVirtualScreen = 76,
		YVirtualScreen = 77,
		VirtualScreenWidth = 78,
		VirtualScreenHeight = 79,
		Monitors = 80,
	}

	internal static class Interop
	{
		public const int WH_KEYBOARD = 2;
		public const int WH_MOUSE = 7;

		[DllImport ("user32.dll")]
		public static extern long SetCursorPos (int x, int y);

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
		public static extern uint MapVirtualKey (uint uCode, VirtualMapType uMapType);

		[DllImport ("user32.dll")]
		public static extern int GetSystemMetrics (SystemMetric metric);
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