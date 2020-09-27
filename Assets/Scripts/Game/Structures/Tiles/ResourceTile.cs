using LiteNetLib;
using Multiplayer.Packets;
using Swindler.Utils;
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
			name.Log("Resource name is");
			GameManager.Server.Send(new PlayerInteractResourcePacket(resourceId, position), DeliveryMethod.Sequenced);
		}

		public override bool CanInterract(Vector3Int position, Tilemap map)
		{
			return IsTopTile(position, map);
		}

		public bool IsTopTile(Vector3Int position, Tilemap map)
		{
			return map.GetTile(new Vector3Int(position.x, position.y - 1, position.z)) == null;
		}
	}
}