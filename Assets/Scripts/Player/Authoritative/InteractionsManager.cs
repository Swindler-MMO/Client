using Swindler.Game.Structures.Tiles;
using Swindler.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player.Authoritative
{
	public class InteractionsManager : MonoBehaviour
	{

		private const float INTERACT_RATE = .5f;
		
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
			nextInteraction = Time.time;
		}

		private void Update()
		{
			position = Utils.MouseToCell(interactionMap);

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
			if (Input.GetMouseButton(0))
				Interact(interactableTile);
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

		private void Interact(InteractableTile interactableTile)
		{

			if (Time.time < nextInteraction)
				return;

			nextInteraction = Time.time + INTERACT_RATE;
			interactableTile.OnInteract(position, audioSource);
		}
		
	}
}