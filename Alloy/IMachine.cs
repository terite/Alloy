using System;
using System.Collections.Generic;

namespace Alloy
{
	/// <summary>
	/// Represents any machine.
	/// </summary>
	public interface IMachine
	{
		/// <summary>
		/// Raised when the screens have changed.
		/// </summary>
		event EventHandler ScreensChanged;

		/// <summary>
		/// Gets the machines current screens.
		/// </summary>
		IEnumerable<Screen> Screens { get; }
	}
}
