using Swindler.Utils;
using System.Threading.Tasks;

namespace Swindler.World.IslandRenderer
{
	public static class IslandAPI
	{

		private const string HOST = "http://localhost:3000";

		public static Task<IslandView> LoadIsland(int x, int y)
		{
			return HttpUtils.Get<IslandView>(HOST + $"/islands/{x}/{y}");
		}

	}
}
