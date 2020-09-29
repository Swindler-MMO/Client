using LiteNetLib.Utils;
using Swindler.Multiplayer;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Multiplayer.Packets.Server
{
	public class PlayerJoinedPacket
	{

		public NetPlayer Player { get; }
		
		public PlayerJoinedPacket(NetDataReader r)
		{
			int playerId = r.GetInt();
			string name = r.GetString();
			Vector2 position = r.GetVector2();
			Player = new NetPlayer(playerId, name, position);
		}
		
	}
}