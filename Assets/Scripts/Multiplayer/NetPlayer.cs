using UnityEngine;

namespace Swindler.Multiplayer
{
	public class NetPlayer
	{

		public int Id { get; }
		public string Name { get; }
		public Vector2 Position { get; }

		public NetPlayer(int id, string name, Vector2 position)
		{
			Id = id;
			Name = name;
			Position = position;
		}
		
	}
}