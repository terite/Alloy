using System;
using System.Runtime.InteropServices;

namespace MonoMac.CoreGraphics
{
	public enum CGError : int
	{
		// Following are available in 10.0 and later.
		Success = 0,
		Failure = 1000,
		IllegalArgument = 1001,
		InvalidConnection = 1002,
		InvalidContext = 1003,
		CannotComplete = 1004,
		NameTooLong = 1005,
		NotImplemented = 1006,
		RangeCheck = 1007,
		TypeCheck = 1008,
		NoCurrentPoint = 1009,
		InvalidOperation = 1010,
		NoneAvailable = 1011,

		// Following are available in 10.2 and later.
		ApplicationRequiresNewerSystem = 1015,
		ApplicationNotPermittedToExecute = 1016,

		// Following are available in 10.3 and later.
		ApplicationIncorrectExecutableFormatFound = 1023,
		ApplicationIsLaunching = 1024,
		ApplicationAlreadyRunning = 1025,
		ApplicationCanOnlyBeRunInOneSessionAtATime = 1026,

		// Following are available in 10.4 and later.
		ClassicApplicationsMustBeLaunchedByClassic = 1027,
		ForkFailed = 1028,

		// Following are available in 10.5 and later.
		RetryRegistration = 1029
	}
}
