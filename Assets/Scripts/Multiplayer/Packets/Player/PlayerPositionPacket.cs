using System;
using System.Diagnostics;
using LiteNetLib.Utils;
using Swindler.Utils;
using UnityEngine;

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
			w.Put(p.x);
			w.Put(p.y);
		}
	}
}