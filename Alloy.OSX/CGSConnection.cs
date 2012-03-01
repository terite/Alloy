using System;
using MonoMac;
using System.Runtime.InteropServices;

namespace MonoMac.CoreGraphics
{
	using MonoMac.CoreFoundation;
	public class CGSConnection
	{

		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static IntPtr _CGSDefaultConnection ();
		public static CGSConnection DefaultConnection()
		{
			return new CGSConnection(_CGSDefaultConnection());
		}

		internal IntPtr Handle;

		public CGSConnection (IntPtr ptr)
		{
			Handle = ptr;
		}

		[DllImport(Constants.CoreGraphicsLibrary)]
		extern static IntPtr CGSSetConnectionProperty (IntPtr self, IntPtr target, IntPtr key, IntPtr value);
		public void SetProperty(CGSConnection target, string property, CFBoolean value)
		{
			var cfProperty = new CFString(property);
			CGSSetConnectionProperty(Handle, target.Handle, cfProperty.Handle, value.Handle);
		}
	}
}
