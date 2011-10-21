using Tempest;

namespace Alloy
{
	public class AlloyClient
		: ClientBase
	{
		public AlloyClient (IClientConnection clientConnection)
			: base (clientConnection, MessageTypes.Reliable)
		{
		}
	}
}