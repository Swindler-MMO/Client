using LiteNetLib;
using Multiplayer.Packets;
using Swindler.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.Game.Structures.Tiles
{
	[CreateAssetMenu(fileName = "Resource Name", menuName = "Swindler/Tiles/Resource")]
	public class ResourceTile : InteractableTile
	{
		public string resourceName;
		public byte resourceId;

		public override void OnInteract(Vector3Int position)
		{
			GameManager.Server.Send(
				new PlayerInteractResourcePacket(resourceId, new Vector2Int(position.x, position.y)),
				DeliveryMethod.Sequenced);
		}

		public override bool CanInteract(Vector3Int position, Tilemap map)
		{
			return IsTopTile(position, map);
		}

		public bool IsTopTile(Vector3Int position, Tilemap map)
		{
			return map.GetTile(new Vector3Int(position.x, position.y - 1, position.z)) == null;
		}
	}
}