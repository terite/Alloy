using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Alloy
{
	internal class ScreenManager
	{
		internal ScreenManager (PositionedScreen serverScreen)
		{
			if (serverScreen == null)
				throw new ArgumentNullException ("serverScreen");

			ActiveScreen = serverScreen;
			UpdateScreens (new[] { serverScreen });
		}

		public event EventHandler<ActiveScreenChangedEventArgs> ActiveScreenChanged;

		internal int Width
		{
			get;
			private set;
		}

		internal int Height
		{
			get;
			private set;
		}

		internal int CursorX
		{
			get;
			private set;
		}

		internal int CursorY
		{
			get;
			private set;
		}

		internal PositionedScreen ActiveScreen
		{
			get;
			private set;
		}

		internal IEnumerable<PositionedScreen> PositionedScreens
		{
			get { return this.positionedScreens; }
		}

		internal void UpdateMouse (int deltaX, int deltaY)
		{
			deltaX = (int)Math.Round (deltaX * ActiveScreen.AlloyXRatio, 0);
			deltaY = (int)Math.Round (deltaY * ActiveScreen.AlloyYRatio, 0);

			int currentX = CursorX + deltaX;
			int currentY = CursorY + deltaY;

			//Trace.WriteLine (String.Format ("D: {0},{1}  C: {2},{3}", deltaX, deltaY, currentX, currentY));

			PositionedScreen screen = GetScreenForCoordinates (currentX, currentY);
			if (screen != null && screen != ActiveScreen)
			{
				int overshotX = (currentX - screen.AlloyX);
				int overshotY = (currentY - screen.AlloyY);

				// We need to readjust the amount overshot into the new
				// screen to account for different screen size ratios
				if (overshotX > 0)
				{
					currentX -= overshotX;
					currentX += (int) Math.Round ((overshotX / ActiveScreen.AlloyXRatio) * screen.AlloyXRatio, 0);
				}

				if (overshotY > 0)
				{
					currentY -= overshotY;
					currentY += (int) Math.Round ((overshotY / ActiveScreen.AlloyYRatio) * screen.AlloyYRatio, 0);
				}

				PositionedScreen oldScreen = ActiveScreen;
				ActiveScreen = screen;
				OnActiveScreenChanged (new ActiveScreenChangedEventArgs (oldScreen));
			}

			CursorX = currentX;
			CursorY = currentY;
		}

		internal PositionedScreen GetScreenForCoordinates (int x, int y)
		{
			PositionedScreen[] screens = this.positionedScreens;
			for (int i = 0; i < screens.Length; ++i)
			{
				PositionedScreen s = screens[i];
				if (s.AlloyX > x || x > s.AlloyX + s.AlloyWidth)
					continue;
				if (s.AlloyY > y || y > s.AlloyY + s.AlloyHeight)
					continue;

				return s;
			}

			return null;
		}

		internal void UpdateScreens (IEnumerable<PositionedScreen> positions)
		{
			if (positions == null)
				throw new ArgumentNullException ("positions");

			HashSet<PositionedScreen> screens = new HashSet<PositionedScreen> (positions);
			if (this.positionedScreens != null)
				screens.UnionWith (this.positionedScreens);

			PositionedScreen right = screens.First();
			PositionedScreen bottom = right;

			foreach (PositionedScreen ps in screens)
			{
				if (ps.AlloyX >= right.AlloyX && ps.Width >= right.Width)
					right = ps;
				if (ps.AlloyY >= bottom.AlloyY && ps.Height >= bottom.Height)
					bottom = ps;
			}

			Width = right.AlloyX + right.AlloyWidth;
			Height = bottom.AlloyY + bottom.AlloyHeight;

			this.positionedScreens = screens.ToArray();
		}

		private PositionedScreen[] positionedScreens;

		private void OnActiveScreenChanged (ActiveScreenChangedEventArgs e)
		{
			var changed = ActiveScreenChanged;
			if (changed != null)
				changed (this, e);
		}
	}

	public class ActiveScreenChangedEventArgs
		: EventArgs
	{
		public ActiveScreenChangedEventArgs (PositionedScreen oldScreen)
		{
			if (oldScreen == null)
				throw new ArgumentNullException ("oldScreen");

			OldScreen = oldScreen;
		}

		public PositionedScreen OldScreen
		{
			get;
			private set;
		}
	}
}