using System.Collections.Generic;
using Swindler.Utils;
using System.Threading.Tasks;
using Swindler.World.IslandRenderer;
using UnityEngine;

namespace Swindler.World
{
	public static class IslandAPI
	{

		private const string HOST = "http://swindler.thebad.xyz";

		public static Task<IslandView> LoadIsland(int x, int y)
		{
			return HttpUtils.Get<IslandView>(HOST + $"/islands/{x}/{y}");
		}

		public static Task<List<Vector2Int>> ListIslands()
		{
			return HttpUtils.Get<List<Vector2Int>>(HOST + "/islands/");
		}

	}
}
