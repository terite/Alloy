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
using System.Drawing;

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
		extern static IntPtr CGEventCreateMouseEvent(IntPtr s, CGEventType t, PointF point, CGMouseButton button);
		public static CGEvent CreateMouseEvent (CGEventSource source, CGEventType type, PointF position, CGMouseButton button)
		{
			IntPtr p = CGEventCreateMouseEvent(source.handle, type, position, button);
			return new CGEvent(p);
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

