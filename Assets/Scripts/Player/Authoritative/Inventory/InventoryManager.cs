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

		public List<Item> Items { get; }

		public InventoryManager()
		{
			Items = new List<Item>();
		}

		public void Add(Item newItem)
		{
			if(!newItem.IsStackable)
				Items.Add(newItem);

			bool wasAdded = false;
			foreach (Item item in Items)
			{
				if(item.Id != newItem.Id || !item.IsStackable)
					continue;

				if(item.StackSize == item.Amount)
					continue;

				if (item.Amount + newItem.Amount <= item.StackSize)
				{
					item.Amount += newItem.Amount;
					wasAdded = true;
					break;
				}
				
				//Complete the stack
				item.Amount = item.StackSize;
				
				//Add the remaining to a new stack
				int exceeding = Mathf.Abs(item.StackSize - (item.Amount + newItem.Amount));
				Items.Add(new Item(item.Id, (ushort) exceeding));
				wasAdded = true;
				break;
			}
			
			if(!wasAdded)
				Items.Add(newItem);
			
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

		private void NotifyChange()
		{
			onInventoryChanged?.Invoke(this, EventArgs.Empty);
		}
	}
	
}