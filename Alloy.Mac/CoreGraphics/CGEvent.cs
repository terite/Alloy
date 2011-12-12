using System;
using System.Runtime.InteropServices;

namespace MonoMac.CoreGraphics
{
	
	class CGEvent
	{
		internal IntPtr handle;
		
		/// <summary>
		/// Creates a CGEvent object pointing to a pre-made Quartz CGEventRef.
		/// </summary>
		/// <param name='reference'>
		/// A Quartz CGEventRef
		/// </param>
		public CGEvent (IntPtr reference)
		{
			if (reference == IntPtr.Zero)
				throw new ArgumentNullException("reference");
			
			handle = reference;
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static IntPtr CGEventCreate (IntPtr source);
		public CGEvent (CGEventSource source)
		{
			throw new NotImplementedException();
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static IntPtr CGEventCreateKeyboardEvent(IntPtr source, UInt16 virtualKey, bool keyDown);
		public static CGEvent CreateKeyboardEvent (CGEventSource source, UInt16 virtualKey, bool keyDown)
		{
			if (source == null)
				throw new ArgumentException("source");
			var e = CGEventCreateKeyboardEvent(source.handle, virtualKey, keyDown);
			return new CGEvent(e);
		}
		public static CGEvent CreateKeyboardEvent(UInt16 virtualKey, bool keyDown)
		{
			return new CGEvent(CGEventCreateKeyboardEvent(IntPtr.Zero, virtualKey, keyDown));
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static void CGEventSetFlags(IntPtr e, CGEventFlags flags);
		public void SetFlags(CGEventFlags flags)
		{
			CGEventSetFlags(handle, flags);
		}
		
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static void CGEventPost(CGEventTapLocation tap, IntPtr e);
		public void Post(CGEventTapLocation tap)
		{
			CGEventPost(tap, handle);
		}
	}

}

