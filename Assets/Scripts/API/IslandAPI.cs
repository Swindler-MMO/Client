using System.Collections.Generic;
using Swindler.Utilities;
using System.Threading.Tasks;
using Swindler.World.Renderers;
using UnityEngine;

namespace Swindler.API
{
	public static class IslandAPI
	{

		private const string HOST = "http://swindler.thebad.xyz";

		public static Task<Island> LoadIsland(int x, int y)
		{
			return HttpUtils.Get<Island>(HOST + $"/islands/{x}/{y}");
		}

		public static Task<List<Vector2Int>> ListIslands()
		{
			return HttpUtils.Get<List<Vector2Int>>(HOST + "/islands/");
		}

	}
}
