/*
 * Copyright © Eric Maupin 2012
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
using Tempest;

namespace Alloy
{
	public struct Position
	{
		public Position (short x, short y)
		{
			X = x;
			Y = y;
		}

		public short X;
		public short Y;
	}

	public enum MouseEventType
		: byte
	{
		LeftDown = 1,
		LeftUp = 2,
		RightDown = 3,
		RightUp = 4,
		Move = 5
	}

	public class MouseEvent
	{
		public MouseEvent (MouseEventType type, Position position)
		{
			Type = type;
			Position = position;
		}

		public MouseEventType Type
		{
			get;
			private set;
		}

		public Position Position
		{
			get;
			private set;
		}
	}

	public class MouseEventSerializer
		: ISerializer<MouseEvent>
	{
		public static readonly MouseEventSerializer Instance = new MouseEventSerializer();

		public void Serialize (ISerializationContext context, IValueWriter writer, MouseEvent element)
		{
			writer.WriteByte ((byte) element.Type);
			writer.WriteInt16 (element.Position.X);
			writer.WriteInt16 (element.Position.Y);
		}

		public MouseEvent Deserialize (ISerializationContext context, IValueReader reader)
		{
			MouseEventType type = (MouseEventType) reader.ReadByte();
			short x = reader.ReadInt16();
			short y = reader.ReadInt16();

			return new MouseEvent (type, new Position (x, y));
		}
	}
}