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
using System.Runtime.InteropServices;

namespace MonoMac.CoreGraphics
{
	class CGEventSource
	{
		internal IntPtr handle;
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static UInt64 CGEventSourceGetTypeID ();
		public static UInt64 GetTypeID ()
		{
			return CGEventSourceGetTypeID();
		}
		
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static IntPtr CGEventSourceCreate (CGEventSourceStateId sourceStateId);
		public CGEventSource(CGEventSourceStateId state)
		{
			handle = CGEventSourceCreate(state);
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static UInt32 CGEventSourceGetKeyboardType(IntPtr source);
		public UInt32 GetKeyboardType ()
		{
			return CGEventSourceGetKeyboardType(handle);
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static void CGEventSourceSetKeyboardType (IntPtr source, UInt32 keyboardType);
		public void SetKeyboardType(UInt32 keyboardType)
		{
			CGEventSourceSetKeyboardType(handle, keyboardType);
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static CGEventSourceStateId CGEventSourceGetSourceStateID (IntPtr source);
		public CGEventSourceStateId GetSourceStateId ()
		{
			return CGEventSourceGetSourceStateID(handle);
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static bool CGEventSourceButtonState (CGEventSourceStateId sourceState, CGMouseButton button);
		public static bool ButtonState(CGEventSourceStateId sourceState, CGMouseButton button)
		{
			return CGEventSourceButtonState (sourceState, button);
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static bool CGEventSourceKeyState (CGEventSourceStateId sourceState, UInt16 key);
		public static bool KeyState (CGEventSourceStateId sourceState, UInt16 key)
		{
			return CGEventSourceKeyState(sourceState, key);
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static CGEventFlags CGEventSourceFlagsState (CGEventSourceStateId sourceState);
		public static CGEventFlags FlagsState (CGEventSourceStateId sourceState)
		{
			return CGEventSourceFlagsState (sourceState);
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static double CGEventSourceSecondsSinceLastEventType (CGEventSourceStateId source, CGEventType eventType);
		public static double SecondsSinceLastEventType (CGEventSourceStateId source, CGEventType eventType)
		{
			return CGEventSourceSecondsSinceLastEventType (source, eventType);
		}
	}
}

