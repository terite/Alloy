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

using Tempest;

namespace Alloy.Messages
{
	public class MouseEventMessage
		: AlloyMessage
	{
		public MouseEventMessage()
			: base (AlloyMessageType.MouseEvent)
		{
		}

		public MouseEvent Event
		{
			get;
			set;
		}

		public override void WritePayload (ISerializationContext context, IValueWriter writer)
		{
			MouseEventSerializer.Instance.Serialize (context, writer, Event);
		}

		public override void ReadPayload (ISerializationContext context, IValueReader reader)
		{
			Event = MouseEventSerializer.Instance.Deserialize (context, reader);
		}
	}
}