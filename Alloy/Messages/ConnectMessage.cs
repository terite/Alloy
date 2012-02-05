﻿/*
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

namespace Alloy.Messages
{
	public class ConnectMessage
		: AlloyMessage
	{
		public ConnectMessage()
			: base (AlloyMessageType.Connect)
		{
		}

		public override bool Encrypted
		{
			get { return true; }
		}

		public string Password
		{
			get;
			set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			writer.WriteString (Password ?? String.Empty);
		}

		public override void ReadPayload(ISerializationContext context, IValueReader reader)
		{
			Password = reader.ReadString();
		}
	}
}