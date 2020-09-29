using LiteNetLib.Utils;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Multiplayer.Packets.Server
{
	public class ResourceRemovedPacket
	{

		public Vector2Int Position { get; }
		
		public ResourceRemovedPacket(NetDataReader r)
		{
			Position = r.GetVector2Int();
		}
		
	}
}