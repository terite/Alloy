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
	public enum KeyboardEventType
		: byte
	{
		Up = 1,
		Down = 2
	}

	public class KeyboardEvent
	{
		public KeyboardEvent (KeyboardEventType type, KeyModifiers modifiers, KeyCode code, byte repeatCount)
		{
			if (repeatCount <= 0)
				throw new ArgumentException ("repeatCount must be greater than 0", "repeatCount");

			Type = type;
			Modifiers = modifiers;
			Code = code;
			RepeatCount = repeatCount;
		}

		public byte RepeatCount
		{
			get;
			private set;
		}

		public KeyboardEventType Type
		{
			get;
			private set;
		}

		public KeyModifiers Modifiers
		{
			get;
			private set;
		}

		public KeyCode Code
		{
			get;
			private set;
		}
	}

	public class KeyboardEventSerializer
		: ISerializer<KeyboardEvent>
	{
		public static readonly KeyboardEventSerializer Instance = new KeyboardEventSerializer();

		public void Serialize (ISerializationContext context, IValueWriter writer, KeyboardEvent element)
		{
			// TODO: combine type and code into single byte
			writer.WriteByte ((byte)element.Type);
			writer.WriteUInt16 ((ushort)element.Modifiers);
			writer.WriteByte ((byte)element.Code);
			writer.WriteByte (element.RepeatCount);
		}

		public KeyboardEvent Deserialize (ISerializationContext context, IValueReader reader)
		{
			KeyboardEventType type = (KeyboardEventType) reader.ReadByte();
			KeyModifiers modifiers = (KeyModifiers) reader.ReadUInt16();
			KeyCode code = (KeyCode) reader.ReadByte();
			byte repeatCount = reader.ReadByte();

			return new KeyboardEvent (type, modifiers, code, repeatCount);
		}
	}
}