using System;
using System.Collections.Generic;

namespace Swindler.Utilities
{
	public class WeightedRandomBag<T>
	{
		private struct Entry
		{
			public double accumulatedWeight;
			public T item;
		}

		private List<Entry> entries;
		private double accumulatedWeight;
		private Random rand;

		public WeightedRandomBag(string seed)
		{
			entries = new List<Entry>();
			rand = new Random(seed.GetHashCode());
		}
		public WeightedRandomBag(Random rd)
		{
			entries = new List<Entry>();
			rand = rd;
		}

		public void AddEntry(T item, double weight)
		{
			accumulatedWeight += weight;
			entries.Add(new Entry { item = item, accumulatedWeight = accumulatedWeight });
		}

		public T GetRandom()
		{
			double r = rand.NextDouble() * accumulatedWeight;

			foreach (Entry entry in entries)
			{
				if (entry.accumulatedWeight >= r)
				{
					return entry.item;
				}
			}
			return default(T); //should only happen when there are no entries
		}
	}
}

