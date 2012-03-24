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
	public static class KeyManager
	{
		public static readonly Func<IPublicKeyCrypto> CryptoFactory = () => new RSACrypto();

		public static Task<IAsymmetricKey> LoadKey (string path)
		{
			if (path == null)
				throw new ArgumentNullException ("path");

			return Task<IAsymmetricKey>.Factory.StartNew (() =>
			{
				if (!File.Exists (path) || new FileInfo (path).Length == 0)
				{
					var crypto = new RSACrypto();
					var key = crypto.ExportKey (includePrivate: true);

					Directory.CreateDirectory (Path.GetDirectoryName (path));
					using (var fstream = File.OpenWrite (path))
					{
						var writer = new StreamValueWriter (fstream);
						key.Serialize (null, writer);
					}

					return key;
				}

				var reader = new BufferValueReader (File.ReadAllBytes (path));
				return new RSAAsymmetricKey (null, reader);
			});
		}
	}
}