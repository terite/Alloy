using System;
using Tempest;

namespace Alloy
{
	public class PositionedScreen
		: Screen
	{
		public PositionedScreen (string name, int height, int width)
			: base (name, height, width)
		{
		}

		public PositionedScreen (Screen screen)
			: base (screen)
		{
		}

		/// <summary>
		/// Gets or sets the X position of this screen on the alloy screen.
		/// </summary>
		public int AlloyX
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the Y position of this screen on the alloy screen.
		/// </summary>
		public int AlloyY
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets width of this screen on the alloy screen.
		/// </summary>
		public int AlloyWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the height of this screen on the alloy screen.
		/// </summary>
		public int AlloyHeight
		{
			get;
			set;
		}
	}

	public class PositionedScreenSerializer
		: ISerializer<PositionedScreen>
	{
		public static readonly PositionedScreenSerializer Instance = new PositionedScreenSerializer();

		public void Serialize (ISerializationContext context, IValueWriter writer, PositionedScreen element)
		{
			if (element == null)
				throw new ArgumentNullException ("element");

			ScreenSerializer.Instance.Serialize (context, writer, element);
			writer.WriteInt32 (element.AlloyX);
			writer.WriteInt32 (element.AlloyY);
			writer.WriteInt32 (element.AlloyWidth);
			writer.WriteInt32 (element.AlloyHeight);
		}

		public PositionedScreen Deserialize (ISerializationContext context, IValueReader reader)
		{
			var ps = new PositionedScreen (ScreenSerializer.Instance.Deserialize (context, reader));
			ps.AlloyX = reader.ReadInt32();
			ps.AlloyY = reader.ReadInt32();
			ps.AlloyWidth = reader.ReadInt32();
			ps.AlloyHeight = reader.ReadInt32();

			return ps;
		}
	}
}
