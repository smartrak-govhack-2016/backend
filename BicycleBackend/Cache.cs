using BicycleBackend.Db;
using BicycleBackend.Routing;

namespace BicycleBackend
{
	/// <summary>
	/// wouldn't be a hackathon without something gross like this
	/// </summary>
	public static class Cache
	{
		public static CrashContext CrashContext = new CrashContext();
		public static readonly NeighborFinder NeighborFinder;
		public static readonly Router Router;

		static Cache()
		{
			NeighborFinder = new NeighborFinder();
			Router = new Router(NeighborFinder);
		}
		public static void DoNothing()
		{ }
	}
}