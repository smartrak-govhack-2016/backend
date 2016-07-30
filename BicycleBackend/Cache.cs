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
		public static readonly Router Router;
		public static readonly CircleRouter CircleRouter;

		static Cache()
		{
			var nf = new NeighborFinder();
			Router = new Router(nf);
			CircleRouter = new CircleRouter(nf);
		}
		public static void DoNothing()
		{ }
	}
}