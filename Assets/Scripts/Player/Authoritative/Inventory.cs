using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Authoritative
{
	public class Inventory : MonoBehaviour
	{

		public Dictionary<ushort, ushort> items;

		private void Awake()
		{
			items = new Dictionary<ushort, ushort>();
		}

		public void Add(ushort itemId, ushort amount)
		{
			if (!items.ContainsKey(itemId))
			{
				items.Add(itemId, amount);
				return;
			}

			items[itemId] += amount;
		}

		public ushort Remove(ushort itemId, ushort amount)
		{
			if (!items.ContainsKey(itemId))
				return 0;

			ushort newAmount = (ushort) Mathf.Max(0, items[itemId] - amount);
			items[itemId] = newAmount;

			return newAmount;
		}
		
		public ushort Remove(ushort itemId)
		{
			items.Remove(itemId);
			return 0;
		}
	}
}