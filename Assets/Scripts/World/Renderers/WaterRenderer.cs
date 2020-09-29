using Swindler.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Swindler.World.Renderers
{
	public class WaterRenderer : MonoBehaviour
	{

		private const int CHUNKS_AROUND_PLAYER = 2;
		
		// private static Matrix4x4[] orientations = new[]
		// {
		// 	Matrix4x4.Rotate(Quaternion.Euler(0f, 0f, 0f)),
		// 	Matrix4x4.Rotate(Quaternion.Euler(0f, 0f, 90f)),
		// 	Matrix4x4.Rotate(Quaternion.Euler(0f, 0f, 180f)),
		// 	Matrix4x4.Rotate(Quaternion.Euler(0f, 0f, -90f)),
		// };

		public Tilemap waterMap;
		public TileBase waterTile;

		//private System.Random random;
		
		public void DrawWater(Vector2 currentPosition)
		{
			//"Drawing Water".Log();
			
			//random = new System.Random(16);

			waterMap.ClearAllTiles();

			int minX = Mathf.RoundToInt(currentPosition.x - CHUNKS_AROUND_PLAYER * WorldManager.CHUNK_SIZE);
			int minY = Mathf.RoundToInt(currentPosition.y - CHUNKS_AROUND_PLAYER * WorldManager.CHUNK_SIZE);
			int maxX = Mathf.RoundToInt(currentPosition.x + CHUNKS_AROUND_PLAYER * WorldManager.CHUNK_SIZE);
			int maxY = Mathf.RoundToInt(currentPosition.y + CHUNKS_AROUND_PLAYER * WorldManager.CHUNK_SIZE);

			// (maxX - minX).Log("X size");
			// (maxY - minY).Log("Y size");
			// ((maxX - minX) * (maxY - minY)).Log("Tilemap size");
			
			//TODO: Optimise this part using BoxFill / SetTiles
			for (int x = minX; x < maxX; x++)
			for (int y = minY; y < maxY; y++)
			{
				Vector3Int pos = new Vector3Int(x, y, 0);
				waterMap.SetTile(pos, waterTile);

				
				//waterMap.SetTransformMatrix(pos, orientations[random.Next(0, 4)]);
			}
		}

	}
}
