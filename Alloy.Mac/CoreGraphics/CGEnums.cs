using System;

namespace MonoMac.CoreGraphics
{
	public enum CGEventSourceStateId : uint
	{
		Private = uint.MaxValue, // originally -1
		CombinedSessionState = 0,
		HIDSystemState = 1
	}
	
	public enum CGMouseButton : uint
	{
		Left = 0,
		Right = 1,
		Center = 2
	}

	/// <summary>
	/// Enum found in CGEventTypes.h, values defined in IOLLEvent.h
	/// </summary>
	public enum CGEventFlags : ulong
	{
		/* Device-independent modifier key bits. */
		AlphaShift   = 0x00010000,
		Shift        = 0x00020000,
		Control      = 0x00040000,
		Alternate    = 0x00080000,
		Command      = 0x00100000,
		
		/* Special key identifiers. */
		Help         = 0x00400000,
		SecondaryFn  = 0x00800000,
		
		/* Identifies key events from numeric keypad area on extended keyboards. */
		NumericPad   = 0x00200000,
		
		/* Indicates if mouse/pen movement events are not being coalesced */
		NonCoalesced = 0x00000100
	}
	public enum CGEventTapLocation : uint
	{
		HIDEventTap = 0,
		SessionEventTap,
		AnnotatedSessionEventTap
	}
}

