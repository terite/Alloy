using System;
using System.Runtime.InteropServices;

namespace MonoMac.CoreFoundation
{
	public class CFBoolean
	{
		internal readonly IntPtr Handle;

		// TODO: We should use kCFBoolean(True|False) instead.
		[DllImport(Constants.CoreGraphicsLibrary)]
		extern internal static IntPtr CGSCreateBoolean (bool b);

		public readonly static CFBoolean False = new CFBoolean(CGSCreateBoolean(false));
		public readonly static CFBoolean True = new CFBoolean(CGSCreateBoolean(true));

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

