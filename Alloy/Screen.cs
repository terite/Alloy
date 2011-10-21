using Tempest;

namespace Alloy
{
	public sealed class Screen
		: ISerializable
	{
		public Screen (int height, int width)
		{
			Height = height;
			Width = width;
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

		public void Serialize (ISerializationContext context, IValueWriter writer)
		{
			writer.WriteInt32 (Height);
			writer.WriteInt32 (Width);
		}

		public void Deserialize (ISerializationContext context, IValueReader reader)
		{
			Height = reader.ReadInt32();
			Width = reader.ReadInt32();
		}
	}
}