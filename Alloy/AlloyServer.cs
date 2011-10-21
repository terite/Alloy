using Tempest;

namespace Alloy
{
	public class AlloyServer
		: ServerBase
	{
		public AlloyServer()
			: base (MessageTypes.Reliable)
		{
		}

		public AlloyServer (IConnectionProvider connectionProvider)
			: base (connectionProvider, MessageTypes.Reliable)
		{
		}
	}
}