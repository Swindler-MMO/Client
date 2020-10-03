using System;
using System.Collections.Generic;
using Swindler.Utilities;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Swindler.Player.Authoritative.Inventory
{
	public class InventoryManager
	{

		public event EventHandler onInventoryChanged;

		public List<ItemStack> Items { get; }

		public InventoryManager()
		{
			Items = new List<ItemStack>();
		}

		public void Add(ItemStack newItemStack)
		{
			if(!newItemStack.IsStackable)
				Items.Add(newItemStack);

			bool wasAdded = false;
			foreach (ItemStack item in Items)
			{
				if(item.Id != newItemStack.Id || !item.IsStackable)
					continue;

				if(item.StackSize == item.Amount)
					continue;

				if (item.Amount + newItemStack.Amount <= item.StackSize)
				{
					item.Amount += newItemStack.Amount;
					wasAdded = true;
					break;
				}

				//Add the remaining to a new stack
				int exceeding = Mathf.Abs(item.StackSize - (item.Amount + newItemStack.Amount));
				Items.Add(new ItemStack(item.Id, (ushort) exceeding));
				
				//Complete the stack
				item.Amount = item.StackSize;
				
				wasAdded = true;
				break;
			}
			
			if(!wasAdded)
				Items.Add(newItemStack);
			
			NotifyChange();
		}

		public void Remove(ushort itemId, ushort amount)
		{
			NotifyChange();
		}
		
		public void Remove(ushort itemId)
		{
			NotifyChange();
		}

		public void Clear()
		{
			Items.Clear();
			NotifyChange();
		}
		
		private void NotifyChange()
		{
			onInventoryChanged?.Invoke(this, EventArgs.Empty);
		}

		
	}
	
}