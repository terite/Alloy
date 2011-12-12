using System;
using MonoMac.AppKit;
using MonoMac.CoreGraphics;
using System.Threading;

namespace Alloy.Mac
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			CGEventType type;
			
			var source = new CGEventSource(CGEventSourceStateId.Private);
			var saveCommandDown = CGEvent.CreateKeyboardEvent(source, 1, true);
			saveCommandDown.SetFlags(CGEventFlags.Command);
			var saveCommandUp = CGEvent.CreateKeyboardEvent(source, 1, false);
			
			// Sleep for testing.
			Thread.Sleep(1000);
			
			saveCommandDown.Post(CGEventTapLocation.AnnotatedSessionEventTap);
			saveCommandUp.Post(CGEventTapLocation.AnnotatedSessionEventTap);
		}
	}
}
