using System;
using Swindler.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Swindler.Player.Authoritative.Inventory
{
	public class InventoryUI : MonoBehaviour
	{
		private InventoryManager inv;
		private Text text;
		
		public void SetInventory(InventoryManager inv, Text text)
		{
			this.text = text;
			this.inv = inv;
			this.inv.onInventoryChanged += OnInventoryUpdate;
		}
		
		public void RefreshUI()
		{
			string content = "";
			foreach (Item item in inv.Items)
			{
				content += $" - {item.Name} x{item.Amount}\n";
			}

			text.text = content;
		}

		private void OnInventoryUpdate(object sender, EventArgs args)
		{
			RefreshUI();
		}
		
	}
}