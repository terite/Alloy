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
	public class Screen
		: IEquatable<Screen>
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

		public override bool Equals(object obj)
		{
			if (ReferenceEquals (null, obj))
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType() != typeof (Screen))
				return false;

			return Equals ((Screen) obj);
		}

		public bool Equals (Screen other)
		{
			if (ReferenceEquals (null, other))
				return false;
			if (ReferenceEquals (this, other))
				return true;

			return Equals (other.Name, this.Name) && other.Height == this.Height && other.Width == this.Width;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = this.Name.GetHashCode();
				result = (result * 397) ^ this.Height;
				result = (result * 397) ^ this.Width;
				return result;
			}
		}

		public static bool operator == (Screen left, Screen right)
		{
			return Equals (left, right);
		}

		public static bool operator != (Screen left, Screen right)
		{
			return !Equals (left, right);
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