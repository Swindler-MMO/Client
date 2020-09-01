using Swindler.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.World.IslandRenderer
{
	class IslandRenderer : MonoBehaviour
	{

		//private const int WATER_AROUND_PLAYER = 20;

		[Header("Tilemaps and tiles")]
		public TileBase[] tiles;
		public SerializableStringTilemap tilemaps;

		//private Vector2 playerPosition = Vector2.zero;
		//private Vector2 lastPosition = Vector2.zero;

		void Start()
		{
			DrawIsland(0, 0);
			//SetPlayerPosition(new Vector2(20, 20));
		}

		public async void DrawIsland(int islandX, int islandY)
		{
			var island = await IslandAPI.LoadIsland(islandX, islandY);

			foreach(IslandLayerView layer in island.Layers)
			{
				Tilemap map = tilemaps[layer.Name];
				for(int x = 0; x < island.Width; x++)
					for (int y = 0; y < island.Height; y++)
						map.SetTile(new Vector3Int(islandX + x, islandY + y, 0), tiles[layer.Data[y * island.Width + x]]);
			}

		}

		//public void SetPlayerPosition(Vector2 pos)
		//{
		//	playerPosition = pos;
		//	DrawWater();
		//}

		private void DrawWater()
		{

			//int minX = Mathf.RoundToInt(playerPosition.x - WATER_AROUND_PLAYER);
			//int minY = Mathf.RoundToInt(playerPosition.y - WATER_AROUND_PLAYER);
			//int maxX = Mathf.RoundToInt(playerPosition.x + WATER_AROUND_PLAYER);
			//int maxY = Mathf.RoundToInt(playerPosition.y + WATER_AROUND_PLAYER);

			//Tilemap waterMap = tilemaps["water"];

			//for (int x = minX; x < maxX; x++)
			//	for (int y = minY; y < maxY; y++)
			//		waterMap.SetTile(new Vector3Int(x, y, 0), waterTile);

		}

	}
}
