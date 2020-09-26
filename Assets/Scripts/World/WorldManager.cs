using System;
using Swindler.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace  Swindler.World
{
	public class WorldManager : MonoBehaviour
	{
		
		public const int CHUNK_SIZE = 16;
		private const int LOAD_ISLAND_THRESHOLD = 20;
		
		[Header("Tilemaps and tiles")]
		public TileBase[] tiles;
		public SerializableStringTilemap tilemaps;
		public TileBase square;
		public TileBase treeTile;
		public TileBase rockTile;

		private QuadTree<IslandPosition> islands;
		private Dictionary<Vector2, GameObject> loadedIslands;
		private bool isListLoaded;

		private void Start()
		{
			islands = new QuadTree<IslandPosition>(10, new Rect(0,0,10000,10000));
			
			loadedIslands = new Dictionary<Vector2, GameObject>();
			
			foreach (var tilemap in tilemaps.Values)
			{
				tilemap.ClearAllTiles();
			}

		}

		private void OnDrawGizmos()
		{
			islands?.DrawDebug();
		}

		public void UpdateWorld(Vector2 position)
		{
			//"Updating world".Log();

			if(!isListLoaded)
				return;

			LoadNearestIslands(position);
			UnloadTooFarIslands(position);
		}

		private void LoadNearestIslands(Vector2 position)
		{
			int distance = CHUNK_SIZE * LOAD_ISLAND_THRESHOLD;
			int halfDistance =distance / 2;
			Rect area = new Rect(position.x - halfDistance, position.y - halfDistance, distance, distance);
			var points = islands.RetrieveObjectsInArea(area);
			foreach (var island in points)
			{
				Vector2 pos = island.GetPosition();
				if (!loadedIslands.ContainsKey(pos))
				{
					CreateIslandRenderer(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
				}
			}
		}

		private void UnloadTooFarIslands(Vector2 position)
		{
			//Copy list in order to remove elements when Destroying
			foreach (var island in loadedIslands.Keys.ToArray())
			{
				float dst = ((position - island) / CHUNK_SIZE).sqrMagnitude;
				if (dst > LOAD_ISLAND_THRESHOLD * LOAD_ISLAND_THRESHOLD)
					DestroyIsland(Vector2Int.RoundToInt(island));
			}
		}
		
		private void DestroyIsland(Vector2Int island)
		{
			Destroy(loadedIslands[island]);
			loadedIslands.Remove(island);
		}
		private void CreateIslandRenderer(int x, int y)
		{
			GameObject go = new GameObject($"Island-{x}-{y}");
			var islandRenderer = go.AddComponent<Renderers.IslandRenderer>();
			
			loadedIslands.Add(new Vector2Int(x, y), go);
			
			islandRenderer.SetRenderData(tiles, tilemaps, square, treeTile, rockTile);
			islandRenderer.SetIsland(x, y);
		}

		private void OnDestroy()
		{
			foreach (var tilemap in tilemaps.Values)
			{
				tilemap.ClearAllTiles();
			}
		}

		public async void LoadIslands(Vector2 position)
		{
			(await IslandAPI.ListIslands()).ForEach(p => islands.Insert(new IslandPosition(p)));
			isListLoaded = true;
			UpdateWorld(position);
		}
		
	}

	public class IslandPosition : IQuadTreeObject
	{
		private Vector2 position;

		public IslandPosition(Vector2Int position)
		{
			this.position = new Vector2(position.x, position.y);
		}

		public Vector2 GetPosition() => position;
	}
	
}
