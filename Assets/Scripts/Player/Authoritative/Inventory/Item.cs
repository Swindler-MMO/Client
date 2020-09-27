using System.Collections.Generic;
using UnityEngine;

namespace Swindler.Player.Authoritative.Inventory
{
	public class Item
	{

		private static Dictionary<ushort, string> NameLookupMap = new Dictionary<ushort, string>()
		{
			{ 0, "Stone" },
			{ 1, "Wood" }
		};
		private static Dictionary<ushort, ushort> StackSizeLookupMap = new Dictionary<ushort, ushort>()
		{
			{ 0, 10 },
			{ 1, 10 }
		};
		
		public ushort Id { get; private set; }
		public string Name { get; private set; }
		public ushort Amount { get; set; }
		public bool IsStackable { get; private set; }
		public ushort StackSize { get; private set; }

		public Item(ushort id, ushort amount = 0, bool isStackable = true)
		{
			Id = id;
			Name = NameLookupMap[id];
			Amount = amount;
			IsStackable = isStackable;

			StackSize = IsStackable ? StackSizeLookupMap[id] : (ushort) 1;
		}
	}
}