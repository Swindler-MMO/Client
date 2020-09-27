using LiteNetLib.Utils;

namespace Multiplayer.Packets
{
	public class PlayerAuthPacket : SwindlerPacket
	{

		private const uint PROTOCOL_VERSION = 0xCAFEBABE;
		
		private string name;
		
		public PlayerAuthPacket(string name)
		{
			this.name = name;
		}
		
		protected override void PerformSerialization(NetDataWriter w)
		{
			w.Put(PROTOCOL_VERSION);
			w.Put(name);
		}
	}
}