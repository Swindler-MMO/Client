using LiteNetLib;
using Multiplayer.Packets;
using Swindler.Utils;
using UnityEngine;

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
	}
}