using LiteNetLib.Utils;
using UnityEngine;

namespace Multiplayer.Packets.Server
{
	public class ResourceMinedPacket
	{
		public Vector2Int Position { get; }
		public ushort ItemId { get; }
		public ushort Amount { get; }

		public ResourceMinedPacket(NetDataReader r)
		{
			Position = new Vector2Int(r.GetInt(), r.GetInt());
			ItemId = r.GetUShort();
			Amount = r.GetUShort();
		}
	}
}