using Swindler.Game;
using Swindler.Game.Structures.Tiles;
using Swindler.Utilities;
using Swindler.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player.Authoritative
{
	public class InteractionsManager : MonoBehaviour
	{
		private static float INTERACT_RATE;

		[Header("Tilemaps and tiles")]
		public Tilemap interactionMap;
		public AnimatedTile highlightTile;
		public Tilemap indicatorsMap;

		public AudioSource audioSource;
		
		private Vector3Int position;
		private Vector3Int lastIndicator = Vector3Int.zero;
		private bool indicatorSet;
		private float nextInteraction;

		private void Awake()
		{
			//This is a weird bug, config is loaded but when defined directly after declaration value is at 0
			INTERACT_RATE = Config.InteractCooldown;
			nextInteraction = Time.time;
		}

		private void Update()
		{
			position = Utils.MouseToCell(interactionMap);

			TileBase tile = interactionMap.GetTile(position);
			
			// Check if indicator needs to be reset
			if (tile == null || (position != lastIndicator && indicatorSet))
				RemoveIndicator();
			
			if (!(tile is InteractableTile))
				return;
			
			//Set indicator when hovering an interactable tile

			InteractableTile interactableTile = (InteractableTile) tile;

			if (!interactableTile.CanInteract(position, interactionMap))
				return;
			
			SetIndicator();
			
			//Handle tile interaction if clicked
			if (Input.GetMouseButton(0))
				Interact(interactableTile);
		}

		private void SetIndicator()
		{
			lastIndicator = position;
			indicatorsMap.SetTile(lastIndicator, highlightTile);
			indicatorSet = true;
		}

		public void RemoveIndicator()
		{
			indicatorsMap.SetTile(lastIndicator, null);
			lastIndicator = Vector3Int.zero;
			indicatorSet = false;
		}

		private void Interact(InteractableTile interactableTile)
		{
			if (Time.time < nextInteraction)
				return;

			nextInteraction = Time.time + INTERACT_RATE;
			interactableTile.OnInteract(position, interactionMap, audioSource);
		}
		
	}
}