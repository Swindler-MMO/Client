using System;
using UnityEngine;

namespace Swindler.World
{
	
	public class WorldUpdater : MonoBehaviour
	{
		
		private const int CHUNK_SIZE = 16;
		private const int UPDATE_DISTANCE_THRESHOLD = 1;
		//private const int WORLD_DISTANCE_THRESHOLD = 3;

		public WaterRenderer waterRenderer;
		public WorldManager worldManager;

		private Vector2 currentPosition;
		private bool wasUpdated = false;

		private void Start()
		{
			worldManager.LoadIslands(transform.position);
		}

		private void Update()
		{
			SetPosition(transform.position);
		}
		
		private void SetPosition(Vector2 position)
		{

			if (!wasUpdated)
			{
				wasUpdated = true;
				currentPosition = position;
				UpdateWorld();
				return;
			}
			
			float chunksMoved = ((position - currentPosition) / CHUNK_SIZE).sqrMagnitude;
			
			//If player has moved further than the distance threshold
			if (chunksMoved >= UPDATE_DISTANCE_THRESHOLD * UPDATE_DISTANCE_THRESHOLD)
			{
				currentPosition = position;
				UpdateWorld();
			}

		}

		private void UpdateWorld()
		{
			waterRenderer.DrawWater(currentPosition);
			worldManager.UpdateWorld(currentPosition);
		}
		
	}
}