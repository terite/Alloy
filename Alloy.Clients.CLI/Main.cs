using System;
using System.Collections.Generic;

using Mono.Options;
using System.Net;

namespace Alloy.Clients.CLI
{
	class MainClass
	{
		public static readonly Dictionary<PlatformID, Func<IMachine>> MachineMap = new Dictionary<PlatformID, Func<IMachine>> ()
		{
			{PlatformID.MacOSX, () => new Alloy.OSX.OSXMachine() },
			{PlatformID.Win32NT, () => new Alloy.Windows.WindowsMachine()}
		};


		// Mono returns Unix for OS X
		// See https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Environment.cs#L238
		private static PlatformID RealPlatform
		{
			get
			{
				var p = Environment.OSVersion.Platform;
				if (p == PlatformID.Unix)
				{
					// TODO: Call Environment.Platform to get real value.
					return PlatformID.MacOSX;
				}
				return p;
			}
		}

		public static void Main (string[] args)
		{
			if (System.Diagnostics.Debugger.IsAttached) {
				if (args.Length == 0)
					args = "client 127.0.0.1".Split(' ');
			}

			Func<IMachine> machineGetter;
			PlatformID platform = RealPlatform;
			if (!MachineMap.TryGetValue(platform, out machineGetter))
				Leave("Platform not supported: " + platform.ToString());

			int port = 42424;
			string password = string.Empty;

			var p = new OptionSet () {
				{ "port", v => port = int.Parse(v) },
				{ "password", v => password = v }
			};
			List<string> extra = p.Parse(args);

			if (extra.Count < 1
			    || (extra[0] != "client" && extra[0] != "server")
			    || (extra[0] == "client" && extra.Count != 2))
			{
				ShowHelp();
				return;
			}

			if (extra[0] == "server")
			{
				var s = new AlloyServer(machineGetter.Invoke());
				if (!string.IsNullOrEmpty(password))
					s.ServerPassword = password;

				var netprovider = new Tempest.Providers.Network.NetworkConnectionProvider(
					AlloyProtocol.Instance,
					new IPEndPoint(IPAddress.Any, port),
					100);

				Console.WriteLine("Listening on {0}:{1}", IPAddress.Any.ToString(), port);
				s.AddConnectionProvider(netprovider);
				s.Start();
				s.ConnectionMade += (sender, e) => {
					Tempest.ConnectionMadeEventArgs ea = e;
					Console.WriteLine("New connection! {0}", ea.ToString());
				};

				while (s.IsRunning)
					continue;

			}
			else
			{
				var c = new AlloyClient(machineGetter.Invoke());
				//if (!string.IsNullOrEmpty(password))
				//	throw new NotImplementedException("Cannot connect using password yet");

				var endpoint = new IPEndPoint(IPAddress.Parse(extra[1]), port);

				var task = c.ConnectAsync(endpoint);
				Console.Write("Generating key and connecting to {0}:{1} ... ", endpoint.Address, endpoint.Port);

				var success = task.Wait(TimeSpan.FromMinutes(2));

				if (!success)
					Leave("Timeout!");

				if (task.Result == null)
					Leave("Failed. Result was null");

				if (!c.IsConnected)
					Leave ("client reports not connected");

				Console.WriteLine("Connected! {0}", task.Result.ToString());
				if (task.Result.Result == Tempest.ConnectionResult.Success)
				{
					Console.WriteLine("Connection success!");
				}

			}
		}

		public static void ShowHelp ()
		{
			Console.WriteLine("Usage: Alloy.Clients.CLI.exe [options] server");
			Console.WriteLine("\tor");
			Console.WriteLine("Usage: Alloy.Clients.CLI.exe [options] client <address>");
			Console.WriteLine(string.Empty);
			Console.WriteLine("Common options:");
			Console.WriteLine("--password <password>\tUse <password> when connecting or hosting.");
			Console.WriteLine("--port <port>\t\tConnect to or or listen on <port>. Default is 42424");
		}

		public static void Leave(string message)
		{
			Leave(message, 1);
		}

		public static void Leave(string message, int code)
		{
			Console.WriteLine(message);
			Environment.Exit(code);
		}
	}
}
