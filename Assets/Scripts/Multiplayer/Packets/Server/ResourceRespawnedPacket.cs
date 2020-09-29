using LiteNetLib.Utils;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Multiplayer.Packets.Server
{
	public class ResourceRespawnedPacket
	{
		public Vector2Int Position { get; }
		public byte Resource { get; }

		public ResourceRespawnedPacket(NetDataReader r)
		{
			Position = r.GetVector2Int();
			Resource = r.GetByte();
		}
	}
}