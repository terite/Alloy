/*
 * Copyright � David Stensland 2012
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
	
	public enum CGEventType : uint
	{
		kCGEventNull = 0,
		kCGEventLeftMouseDown = 1,
		kCGEventLeftMouseUp = 2,
		kCGEventRightMouseDown = 3,
		kCGEventRightMouseUp = 4,
		kCGEventMouseMoved = 5,
		kCGEventLeftMouseDragged = 6,
		kCGEventRightMouseDragged = 7,
		kCGEventKeyDown = 10,
		kCGEventKeyUp = 11,
		kCGEventFlagsChanged = 12,
		kCGEventScrollWheel = 22,
		kCGEventTabletPointer = 23,
		kCGEventTabletProximity = 24,
		kCGEventOtherMouseDown = 25,
		kCGEventOtherMouseUp = 26,
		kCGEventOtherMouseDragged = 27,
		kCGEventTapDisabledByTimeout = 0xFFFFFFFE,
		kCGEventTapDisabledByUserInput = 0xFFFFFFFF
	}
}

