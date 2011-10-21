using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tempest;

namespace Alloy.Messages
{
	public sealed class ScreensChangedMessage
		: AlloyMessage
	{
		public ScreensChangedMessage (IEnumerable<Screen> screens)
			: this()
		{
			if (screens == null)
				throw new ArgumentNullException ("screens");

			Screens = screens;
		}

		private ScreensChangedMessage()
			: base (AlloyMessageType.ScreensChanged)
		{
		}

		public IEnumerable<Screen> Screens
		{
			get;
			private set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			
		}

		public override void ReadPayload (ISerializationContext context, IValueReader reader)
		{
			
		}
	}
}