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
using System.IO;
using System.Threading.Tasks;
using Tempest;

namespace Alloy.Client
{
	public class KeyManager
	{
		public Task<RSAAsymmetricKey> LoadKey (string path)
		{
			return Task<RSAAsymmetricKey>.Factory.StartNew (() =>
			{
				if (!File.Exists (path))
					return null;

				var reader = new BufferValueReader (File.ReadAllBytes (path));
				return new RSAAsymmetricKey (null, reader);
			});
		}

		public Task SaveKey (RSAAsymmetricKey key, string path)
		{
			return Task.Factory.StartNew (s =>
			{
				RSAAsymmetricKey k = (RSAAsymmetricKey) s;

				using (var fstream = File.OpenWrite (path))
				{
					var writer = new StreamValueWriter (fstream);
					k.Serialize (null, writer);
				}
			}, key);
		}
	}
}