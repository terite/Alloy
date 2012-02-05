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
	public enum AlloyMessageType
		: ushort
	{
		Connect = 1,
		ConnectResult = 2,
		ScreenChanged = 3,
		MouseEvent = 4,
		KeyboardEvent = 5,
		MachineState = 6
	}

	public abstract class AlloyMessage
		: Message
	{
		protected AlloyMessage (AlloyMessageType messageType)
			: base (AlloyProtocol.Instance, (ushort)messageType)
		{
		}
	}
}