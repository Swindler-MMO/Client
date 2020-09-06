using Swindler.Utils;
using System;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.World.IslandRenderer
{
	class IslandRenderer : MonoBehaviour
	{

		[Header("Tilemaps and tiles")]
		public TileBase[] tiles;
		public SerializableStringTilemap tilemaps;

		private int x;
		private int y;
		private int width;
		private int height;
		private string[] layers;

		public void SetRenderData(TileBase[] tiles, SerializableStringTilemap tilemaps)
		{
			this.tiles = tiles;
			this.tilemaps = tilemaps;
		}

		public async void SetIsland(int x, int y)
		{
			var island = await IslandAPI.LoadIsland(x, y);

			this.x = x;
			this.y = y;
			width = island.Width;
			height = island.Height;
			layers = island.Layers.Select(layer => layer.Name).ToArray();

			DrawIsland(island, x, y);
			//TODO: Create colliders
		}

		private void DrawIsland(IslandView island, int islandX, int islandY)
		{
			if (tiles == null || tilemaps == null)
				throw new Exception("Tilemaps or tiles not assigned, use SetRenderData before calling SetIsland"); 

			foreach (IslandLayerView layer in island.Layers)
			{
				Tilemap map = tilemaps[layer.Name];
				for (int x = 0; x < island.Width; x++)
					for (int y = 0; y < island.Height; y++)
						map.SetTile(new Vector3Int(islandX + x, islandY + y, 0), tiles[layer.Data[y * island.Width + x]]);
			}

		}

		private void OnDestroy()
		{

			//If island is not loaded, return
			if (layers == null || layers.Length == 0)
				return;

			foreach (string layer in layers)
			{
				Tilemap map = tilemaps[layer];

				//If Unity remove Tilemap before this object, continue
				if (map == null)
					continue;

				for (int i = 0; i < width; i++)
					for (int j = 0; j < height; j++)
						map.SetTile(new Vector3Int(x + i, y + j, 0), null);
			}

			//TODO: Clear colliders
		}


	}
}
