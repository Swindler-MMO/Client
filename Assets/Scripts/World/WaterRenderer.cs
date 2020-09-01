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
	class WaterRenderer : MonoBehaviour
	{

		private const int CHUNK_SIZE = 16;
		private const int CHUNKS_AROUND_PLAYER = 2;
		private const int DISTANCE_THRESHOLD = 1;

		public Tilemap waterMap;
		public TileBase waterTile;

		private Vector2 currentPosition = Vector2.zero;
		private bool wasUpdated = false;

		private void Update()
		{
			SetPosition(transform.position);
		}

		public void SetPosition(Vector2 position)
		{
			if(!wasUpdated)
			{
				wasUpdated = true;
				currentPosition = position;
				DrawWater();
				return;
			}

			if(((position - currentPosition) / 16).sqrMagnitude >= DISTANCE_THRESHOLD * DISTANCE_THRESHOLD)
			{
				currentPosition = position;
				DrawWater();
			}

		}

		private void DrawWater()
		{
			"Drawing Water".Log();

			waterMap.ClearAllTiles();

			int minX = Mathf.RoundToInt(currentPosition.x - CHUNKS_AROUND_PLAYER * CHUNK_SIZE);
			int minY = Mathf.RoundToInt(currentPosition.y - CHUNKS_AROUND_PLAYER * CHUNK_SIZE);
			int maxX = Mathf.RoundToInt(currentPosition.x + CHUNKS_AROUND_PLAYER * CHUNK_SIZE);
			int maxY = Mathf.RoundToInt(currentPosition.y + CHUNKS_AROUND_PLAYER * CHUNK_SIZE);

			for (int x = minX; x < maxX; x++)
				for (int y = minY; y < maxY; y++)
					waterMap.SetTile(new Vector3Int(x, y, 0), waterTile);
		}

	}
}
