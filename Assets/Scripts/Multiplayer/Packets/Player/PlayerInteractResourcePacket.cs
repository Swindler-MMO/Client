using LiteNetLib.Utils;
using UnityEngine;
using Swindler.Utilities.Extensions;

namespace Multiplayer.Packets
{
	public class PlayerInteractResourcePacket : SwindlerPacket
	{
		private const short PACKET_ID = 2;

		private Vector2Int position;
		private byte resourceId;

		public PlayerInteractResourcePacket(byte resourceId, Vector2Int position)
		{
			this.resourceId = resourceId;
			this.position = position;
		}

		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(PACKET_ID);

			//We only need x,y coordinates
			w.Put(position);

			w.Put(resourceId);
		}
	}
}