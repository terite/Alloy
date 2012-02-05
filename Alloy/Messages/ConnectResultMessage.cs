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

using Tempest;

namespace Alloy.Messages
{
	public enum ConnectResult
		: byte
	{
		FailedUnknown = 0,
		Success = 1,
		FailedPassword = 2,
	}

	public class ConnectResultMessage
		: AlloyMessage
	{
		public ConnectResultMessage()
			: base (AlloyMessageType.ConnectResult)
		{
		}

		public ConnectResult Result
		{
			get;
			set;
		}

		public bool KeyboardEncryption
		{
			get;
			set;
		}

		public bool MouseEncryption
		{
			get;
			set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			writer.WriteByte ((byte) Result);
			writer.WriteBool (KeyboardEncryption);
			writer.WriteBool (MouseEncryption);
		}

		public override void ReadPayload(ISerializationContext context, IValueReader reader)
		{
			Result = (ConnectResult)reader.ReadByte();
			KeyboardEncryption = reader.ReadBool();
			MouseEncryption = reader.ReadBool();
		}
	}
}