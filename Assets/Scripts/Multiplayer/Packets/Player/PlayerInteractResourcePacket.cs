using LiteNetLib.Utils;
using UnityEngine;

namespace Multiplayer.Packets
{
	public class PlayerInteractResourcePacket : SwindlerPacket
	{
		private const short PACKET_ID = 2;

		private Vector3Int position;
		private byte resourceId;

		public PlayerInteractResourcePacket(byte resourceId, Vector3Int position)
		{
			this.resourceId = resourceId;
			this.position = position;
		}

		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(PACKET_ID);

			//We only need x,y coordinates
			w.Put(position.x);
			w.Put(position.y);

			w.Put(resourceId);
		}
	}
}