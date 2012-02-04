using System;
using System.Collections.Generic;
using System.Linq;

namespace Alloy
{
	public class AlloyScreen
	{
		public AlloyScreen (IEnumerable<PositionedScreen> positionedScreens)
		{
			if (positionedScreens == null)
				throw new ArgumentNullException ("positionedScreens");

			Update (positionedScreens);
		}

		public int Width
		{
			get;
			private set;
		}

		public int Height
		{
			get;
			private set;
		}

		public IEnumerable<PositionedScreen> PositionedScreens
		{
			get;
			private set;
		}

		public void Update (IEnumerable<PositionedScreen> positionedScreens)
		{
			lock (this.sync)
			{
				PositionedScreen[] screens = positionedScreens.ToArray();

				PositionedScreen right = screens[0];
				PositionedScreen bottom = screens[0];

				for (int i = 1; i < screens.Length; ++i)
				{
					PositionedScreen ps = screens[i];

					if (ps.AlloyX >= right.AlloyX && ps.Width >= right.Width)
						right = ps;
					if (ps.AlloyY >= bottom.AlloyY && ps.Height >= bottom.Height)
						bottom = ps;
				}

				Width = right.AlloyX + right.AlloyWidth;
				Height = bottom.AlloyY + bottom.AlloyHeight;

				positionedScreens = screens;
			}
		}

		private readonly object sync = new object();
	}
}