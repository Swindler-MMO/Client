using Swindler.Utilities;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.World.Renderers
{
	public class IslandRenderer : MonoBehaviour
	{

		private const string PROPS_LAYER = "props";
		
		[Header("Tilemaps and tiles")]
		public TileBase[] tiles;
		public TileBase square;
		public SerializableStringTilemap tilemaps;
		public TileBase treeTile;
		public TileBase rockTile;
		
		private int x;
		private int y;
		private int width;
		private int height;
		private string[] layers;

		public void SetRenderData(TileBase[] tiles, SerializableStringTilemap tilemaps, TileBase square, TileBase treeTile, TileBase rockTile)
		{
			this.tiles = tiles;
			this.tilemaps = tilemaps;
			this.square = square;
			this.treeTile = treeTile;
			this.rockTile = rockTile;
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

		private void DrawIsland(Island island, int islandX, int islandY)
		{
			if (tiles == null || tilemaps == null)
				throw new Exception("Tilemaps or tiles not assigned, use SetRenderData before calling SetIsland");
			
			TileBase[] tilesArray = new TileBase[width * height];
			Vector3Int[] positions = new Vector3Int[width * height];
			
			foreach (IslandLayer layer in island.Layers)
			{
				Tilemap map = tilemaps[layer.Name];

				if (layer.Name == PROPS_LAYER)
				{
					for (int x = 0; x < width; x++)
					for (int y = 0; y < height; y++)
					{
						int tile = layer.Data[y * island.Width + x];
						if (tile >= 8 && tile <= 10)
						{
							//Trees need 1 tile up
							map.SetTile(new Vector3Int(islandX + x, islandY + y, 0), treeTile);
							map.SetTile(new Vector3Int(islandX + x, islandY + y + 1, 0), treeTile);
							
							continue;
						}
						
						if (tile >= 5 && tile <= 7)
						{
							//Rocks need 1 tile up
							map.SetTile(new Vector3Int(islandX + x, islandY + y, 0), rockTile);
							map.SetTile(new Vector3Int(islandX + x, islandY + y + 1, 0), rockTile);
							continue;
						}
						
						// if (tile > 0)
						// {
						// 	map.SetTile(new Vector3Int(islandX + x, islandY + y, 0), tiles[tile]);
						// }
					}

					continue;
				}
				
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

		private void GenerateColliders(Island island, int islandX, int islandY)
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

			//$"Destroying island {gameObject.name}".Log();
			
			//If island is not loaded, return
			if (layers == null || layers.Length == 0)
				return;

			foreach (string layer in layers)
			{
				Tilemap map = tilemaps[layer];

				//If Unity has removed Tilemap before this object, continue
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
