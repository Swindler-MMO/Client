using System.Collections;
using System.Collections.Generic;
using Swindler.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.Game.Structures.Tiles
{
	[CreateAssetMenu]
	public abstract class InteractableTile : RuleTile<InteractableTile.Neighbor> {
		
		public class Neighbor : RuleTile.TilingRule.Neighbor {
			public const int Null = 3;
			public const int NotNull = 4;
		}

		public override bool RuleMatch(int neighbor, TileBase tile) {
			switch (neighbor) {
				case Neighbor.Null: return tile == null;
				case Neighbor.NotNull: return tile != null;
			}
			return base.RuleMatch(neighbor, tile);
		}

		public abstract void OnInteract(Vector3Int position, Tilemap map, AudioSource audioSource);
		public abstract bool CanInteract(Vector3Int position, Tilemap map);
	}
}