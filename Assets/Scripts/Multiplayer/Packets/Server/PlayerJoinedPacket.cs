using LiteNetLib.Utils;
using Swindler.Multiplayer;
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
			Vector2 position = new Vector2(r.GetFloat(), r.GetFloat());
			Player = new NetPlayer(playerId, name, position);
		}
		
	}
}