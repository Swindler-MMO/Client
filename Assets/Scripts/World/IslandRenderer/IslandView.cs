using System.Collections.Generic;
using UnityEngine;

namespace Swindler.World.IslandRenderer
{
	public class IslandView
	{

		/// <summary>
		/// Island top left corner
		/// </summary>
		public Vector2 TLCorner { get; set;  }
		/// <summary>
		/// Island bottom right corner
		/// </summary>
		public Vector2 BRCorner { get; set; }
		/// <summary>
		/// Island center
		/// </summary>
		public Vector2 Center { get; set; }
		/// <summary>
		/// Width of this island
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Height of this island
		/// </summary>
		public int Height { get; set; }

		public List<IslandLayerView> Layers { get; set; }

	}

	public class IslandLayerView
	{

		public string Name { get; }
		public int[] Data { get; }

		public IslandLayerView(string name, int[] data)
		{
			Name = name;
			Data = data;
		}

		public int GetLength(int dimension)
		{
			return Data.GetLength(dimension);
		}

	}

}
