using System.Collections.Generic;
using UnityEngine;

namespace Swindler.World.Renderers
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Island
	{

		/// <summary>
		/// Island top left corner
		/// </summary>
		public Vector2Int TLCorner { get; set;  }
		/// <summary>
		/// Island bottom right corner
		/// </summary>
		public Vector2Int BRCorner { get; set; }
		/// <summary>
		/// Island center
		/// </summary>
		public Vector2Int Center { get; set; }
		/// <summary>
		/// Width of this island
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Height of this island
		/// </summary>
		public int Height { get; set; }

		public List<IslandLayer> Layers { get; set; }

	}

	public class IslandLayer
	{

		public string Name { get; }
		public int[] Data { get; }

		public IslandLayer(string name, int[] data)
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
