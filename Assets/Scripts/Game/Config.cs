using System.Collections.Generic;
using Swindler.Game.Structures;
using Swindler.Utilities.Extensions;

namespace Swindler.Game
{
	public static class Config
	{
		public static float InteractCooldown { get; private set; }
		public static float MovementUpdateTime { get; private set; }
		public static string GameServerHost { get; private set; }

		public static Dictionary<ushort, Item> Items { get; private set; }
		
		public static void FromView(ConfigView v)
		{
			InteractCooldown = v.InteractCooldown;
			MovementUpdateTime = v.MovementUpdateTime;
			GameServerHost = v.GameServerHost;
			Items = new Dictionary<ushort, Item>();
			foreach (Item item in v.Items)
				Items.Add(item.Id, item);
		}
		
	}

	public class ConfigView
	{
		public float InteractCooldown { get; set; }
		public float MovementUpdateTime { get; set; }
		public string GameServerHost { get; set; }
		public List<Item> Items { get; set; }
	}
}