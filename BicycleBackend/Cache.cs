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
        public static Router Router = new Router(new NeighborFinder());

        public static void DoNothing()
        { }
    }
}