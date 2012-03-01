using System;
using System.Runtime.InteropServices;

namespace MonoMac.CoreFoundation
{
	public class CFBoolean
	{
		public IntPtr Handle;

		[DllImport(Constants.CoreGraphicsLibrary)]
		extern internal static IntPtr CGSCreateBoolean (bool b);

		public static CFBoolean False
		{
			get { return new CFBoolean(CGSCreateBoolean(false)); }

		}
		public static CFBoolean True
		{
			get { return new CFBoolean(CGSCreateBoolean(true)); }
		}

		public CFBoolean (IntPtr handle)
		{
			Handle = handle;
		}

		public static explicit operator CFBoolean (bool from)
		{
			return from ? CFBoolean.True : CFBoolean.False;
		}
	}
}

