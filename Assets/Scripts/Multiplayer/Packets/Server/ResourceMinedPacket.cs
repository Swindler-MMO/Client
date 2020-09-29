using LiteNetLib.Utils;
using Swindler.Utilities.Extensions;
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
			Position = r.GetVector2Int();
			ItemId = r.GetUShort();
			Amount = r.GetUShort();
		}
	}
}