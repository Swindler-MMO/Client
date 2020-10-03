using LiteNetLib.Utils;
using Swindler.Game;
using Swindler.Player.Authoritative.Inventory;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Multiplayer.Packets.Server
{
	public class ResourceMinedPacket
	{
		public Vector2Int Position { get; }
		public ItemStack ItemStack { get; }

		public ResourceMinedPacket(NetDataReader r)
		{
			Position = r.GetVector2Int();
			ItemStack = ItemStack.FromItem(Config.Items[r.GetUShort()], r.GetUShort());
		}
	}
}