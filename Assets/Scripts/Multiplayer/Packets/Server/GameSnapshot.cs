using System.Collections.Generic;
using LiteNetLib.Utils;
using Swindler.Multiplayer;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Multiplayer.Packets.Server
{
	public class GameSnapshot
	{

		public Dictionary<int, Vector2> Positions { get; }

		public GameSnapshot(NetDataReader r)
		{
			Positions = new Dictionary<int, Vector2>();

			int playerCount = r.GetInt();

			for (int i = 0; i < playerCount; i++)
				Positions.Add(r.GetInt(), r.GetVector2());
		}
		
		
	}
}