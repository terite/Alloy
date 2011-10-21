using Tempest;

namespace Alloy
{
	public static class AlloyProtocol
	{
		public static readonly Protocol Instance = new Protocol (237);

		static AlloyProtocol()
		{
			Instance.DiscoverFromAssemblyOf<AlloyServer>();
		}
	}
}