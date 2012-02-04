using System;
using Tempest;

namespace Alloy
{
	public class Screen
	{
		public Screen (string name, int height, int width)
		{
			Name = name;
			Height = height;
			Width = width;
		}

		public Screen (Screen screen)
		{
			if (screen == null)
				throw new ArgumentNullException ("screen");

			Name = screen.Name;
			Height = screen.Height;
			Width = screen.Width;
		}

		/// <summary>
		/// Gets the name of the screen.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the height of the screen in pixels.
		/// </summary>
		public int Height
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the width of the screen in pixels.
		/// </summary>
		public int Width
		{
			get;
			private set;
		}
	}

	internal sealed class ScreenSerializer
		: ISerializer<Screen>
	{
		public static readonly ScreenSerializer Instance = new ScreenSerializer();

		public void Serialize (ISerializationContext context, IValueWriter writer, Screen element)
		{
			if (element == null)
				throw new ArgumentNullException ("element");

			writer.WriteString (element.Name);
			writer.WriteInt32 (element.Height);
			writer.WriteInt32 (element.Width);
		}

		public Screen Deserialize (ISerializationContext context, IValueReader reader)
		{
			return new Screen (reader.ReadString(), reader.ReadInt32(), reader.ReadInt32());
		}
	}
}