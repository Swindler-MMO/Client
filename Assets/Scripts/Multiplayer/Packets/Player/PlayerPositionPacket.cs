using LiteNetLib.Utils;
using UnityEngine;
using Swindler.Utilities.Extensions;

namespace Multiplayer.Packets
{
	public class PlayerPositionPacket : SwindlerPacket
	{

		private const short PACKET_ID = 1;
		
		private Vector2 p;

		public PlayerPositionPacket(Vector3 p)
		{
			this.p = p;
		}

		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(PACKET_ID);
			w.Put(p);
		}
	}
}