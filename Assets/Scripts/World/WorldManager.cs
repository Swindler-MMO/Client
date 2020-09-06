using Swindler.Utils;
using Swindler.World.IslandRenderer;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace  Swindler.World
{
	class WorldManager : MonoBehaviour
	{

		[Header("Tilemaps and tiles")]
		public TileBase[] tiles;
		public SerializableStringTilemap tilemaps;
		public TileBase square;

		private void Start()
		{
			//This is a temporary, we will later need to fetch islands around player and them instanciates renderer
			CreateIslandRenderer(0, 0);

			//CreateIslandRenderer(50, 50);
			//CreateIslandRenderer(-50, 50);
			//CreateIslandRenderer(50, -50);
			//CreateIslandRenderer(-50, -50);
		}

		private void CreateIslandRenderer(int x, int y)
		{
			GameObject go = new GameObject($"Island-{x}-{y}");
			var renderer = go.AddComponent<IslandRenderer.IslandRenderer>();
			renderer.SetRenderData(tiles, tilemaps, square);
			renderer.SetIsland(x, y);
		}

		public void DestoryIsland(int x, int y)
		{
			//TODO: Improve by using a list of laoded islands instead of Find
			Destroy(GameObject.Find($"Island-{x}-{y}"));
		}

	}
}
