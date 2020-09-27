using LiteNetLib.Utils;
using UnityEngine;

namespace Multiplayer.Packets.Server
{
	public class ResourceRespawnedPacket
	{
		public Vector2Int Position { get; }
		public byte Resource { get; }

		public ResourceRespawnedPacket(NetDataReader r)
		{
			Position = new Vector2Int(r.GetInt(), r.GetInt());
			Resource = r.GetByte();
		}
	}
}