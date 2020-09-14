using Swindler.Utils;
using System;
using System.Diagnostics;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.World.IslandRenderer
{
	public class IslandRenderer : MonoBehaviour
	{

		[Header("Tilemaps and tiles")]
		public TileBase[] tiles;
		public TileBase square;
		public SerializableStringTilemap tilemaps;

		private int x;
		private int y;
		private int width;
		private int height;
		private string[] layers;

		public void SetRenderData(TileBase[] tiles, SerializableStringTilemap tilemaps, TileBase square)
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
			
			//GenerateColliders(island, x, y);
			//TODO: Create colliders
		}

		private void DrawIsland(IslandView island, int islandX, int islandY)
		{
			if (tiles == null || tilemaps == null)
				throw new Exception("Tilemaps or tiles not assigned, use SetRenderData before calling SetIsland");
			
			TileBase[] tilesArray = new TileBase[width * height];
			Vector3Int[] positions = new Vector3Int[width * height];
			
			foreach (IslandLayerView layer in island.Layers)
			{
				Tilemap map = tilemaps[layer.Name];

				for (int x = 0; x < width; x++)
					for (int y = 0; y < height; y++)
					{
						int index = x * width + y;
						positions[index] = new Vector3Int(islandX + x, islandY + y, 0);
						tilesArray[index] = tiles[layer.Data[y * island.Width + x]];
					}
				map.SetTiles(positions, tilesArray);
			}

		}

		private void GenerateColliders(IslandView island, int islandX, int islandY)
		{

			var layer = island.Layers.Find(l => l.Name == "island");
			Tilemap map = tilemaps["props"];
			for (int x = 0; x < island.Width; x++)
				for (int y = 0; y < island.Height; y++)
					if (IsShoreTile(x, y, island.Width, layer.Data))
					{
						map.SetTile(new Vector3Int(islandX + x, islandY + y, 0), square);
					}

		}

		private bool IsShoreTile(int mapX, int mapY, int width, int[] data)
		{
			//Check only water tile
			if (data[mapY * width + mapX] != 0)
				return false;

			for (int x = mapX - 1; x < mapX + 1; x++)
				for (int y = mapY - 1; x < mapY + 1; y++)
				{
					int index = y * width + x;
					if (index < 0 || index >= data.Length)
						continue;

					if (data[index] > 0)
						return true;
				}

			return false;
		}

		private void OnDestroy()
		{

			("Destroying island " + gameObject.name).Log();
			
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
