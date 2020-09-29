using System;
using Swindler.Game.Structures;
using Swindler.Game.Structures.Tiles;
using Swindler.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player.Authoritative
{
	public class InteractionsManager : MonoBehaviour
	{
		public Tilemap interactionMap;
		public Color highlightColor = new Color32(255, 235, 153, 255);
		public AnimatedTile highlightTile;
		public Tilemap indicatorsMap;

		private Vector3Int position;
		private Vector3Int lastIndicator = Vector3Int.zero;
		private bool indicatorSet = false;

		private void Update()
		{
			position = Swindler.Utilities.Utils.MouseToCell(interactionMap);

			// Check if indicator needs to be reset
			if (position != lastIndicator && indicatorSet)
				RemoveIndicator();


			TileBase tile = interactionMap.GetTile(position);
			if (!(tile is InteractableTile))
				return;
			
			//Set indicator when hovering an interactable tile

			InteractableTile interactableTile = (InteractableTile) tile;

			if (!interactableTile.CanInteract(position, interactionMap))
				return;
			
			SetIndicator();
			
			//Handle tile interaction if clicked
			if(Input.GetMouseButtonDown(0))
				interactableTile.OnInteract(position);
		}

		private void SetIndicator()
		{
			lastIndicator = position;
			indicatorsMap.SetTile(lastIndicator, highlightTile);
			indicatorSet = true;
		}

		private void RemoveIndicator()
		{
			indicatorsMap.SetTile(lastIndicator, null);
			lastIndicator = Vector3Int.zero;
			indicatorSet = false;
		}
		
	}
}