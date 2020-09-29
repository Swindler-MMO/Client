using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace Swindler.Utilities
{
	[System.Serializable]
	public class SerializableStringTilemap : SerializableDictionaryBase<string, Tilemap>
	{

		public Dictionary<string, Tilemap> ToDictionary()
		{
			Dictionary<string, Tilemap> maps = new Dictionary<string, Tilemap>();
			foreach (string key in Keys)
				maps.Add(key, this[key]);
			return maps;
		}
	}
}
