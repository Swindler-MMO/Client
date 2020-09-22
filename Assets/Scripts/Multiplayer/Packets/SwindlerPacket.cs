using LiteNetLib.Utils;

namespace Multiplayer.Packets
{
	public abstract class SwindlerPacket
	{

		private readonly NetDataWriter w;
		
		protected SwindlerPacket()
		{
			w = new NetDataWriter();
		}

		public NetDataWriter Serialize()
		{
			PerformSerialization(w);
			return w;
		}
		
		protected abstract void PerformSerialization(NetDataWriter w);
	}
}