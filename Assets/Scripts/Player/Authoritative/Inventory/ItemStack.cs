using System.Collections.Generic;
using Swindler.Game.Structures;
using UnityEngine;

namespace Swindler.Player.Authoritative.Inventory
{
	public class ItemStack : Item
	{
		public ushort Amount { get; set; }

		public ItemStack(ushort id, string name, ushort stackSize, ushort amount = 0,  bool isStackable = true)
			: base(id, name, stackSize, isStackable)
		{
			Amount = amount;
		}

		public ItemStack(ushort id, ushort amount)
			: base(id)
		{
			Amount = amount;
		}
		
		public static ItemStack FromItem(Item i, ushort amount = 0)
		{
			return new ItemStack(i.Id, i.Name, i.StackSize, amount, i.IsStackable);
		}

		public override string ToString()
		{
			return $"{{ItemStack: {Name} ({Id}) x{Amount}}}";
		}
	}
}