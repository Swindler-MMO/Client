using LiteNetLib.Utils;

namespace Multiplayer.Packets.Server
{
	public class PlayerLeftPacket
	{

		public int Id { get; }
		
		public PlayerLeftPacket(NetDataReader r)
		{
			Id = r.GetInt();
		}
		
	}
}