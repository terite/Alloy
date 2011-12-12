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
		public static double SecondsSinceLastEventType (CGEventSourceGetTypeID source, CGEventType eventType)
		{
			return CGEventSourceSecondsSinceLastEventType (source, eventType);
		}
	}
}

