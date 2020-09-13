using Swindler.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.World
{
	public class WaterRenderer : MonoBehaviour
	{

		private const int CHUNKS_AROUND_PLAYER = 2;

		public Tilemap waterMap;
		public TileBase waterTile;
		
		public void DrawWater(Vector2 currentPosition)
		{
			"Drawing Water".Log();

			waterMap.ClearAllTiles();

			int minX = Mathf.RoundToInt(currentPosition.x - CHUNKS_AROUND_PLAYER * WorldManager.CHUNK_SIZE);
			int minY = Mathf.RoundToInt(currentPosition.y - CHUNKS_AROUND_PLAYER * WorldManager.CHUNK_SIZE);
			int maxX = Mathf.RoundToInt(currentPosition.x + CHUNKS_AROUND_PLAYER * WorldManager.CHUNK_SIZE);
			int maxY = Mathf.RoundToInt(currentPosition.y + CHUNKS_AROUND_PLAYER * WorldManager.CHUNK_SIZE);

			for (int x = minX; x < maxX; x++)
				for (int y = minY; y < maxY; y++)
					waterMap.SetTile(new Vector3Int(x, y, 0), waterTile);
		}

	}
}
