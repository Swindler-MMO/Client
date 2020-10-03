using System.Collections.Generic;
using LiteNetLib.Utils;
using Swindler.Game;
using Swindler.Multiplayer;
using Swindler.Utilities;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Multiplayer.Packets.Server
{
	public class InitialSetupPacket
	{

		public int PlayerId { get; }
		public List<NetPlayer> Players { get; }

		public InitialSetupPacket(NetDataReader r)
		{
			PlayerId = r.GetInt();
			Players = new List<NetPlayer>();

			int playersCount = r.GetInt();
			for (int i = 0; i < playersCount; i++)
			{
				Players.Add(r.GetNetPlayer());
			}

			int nodesToRemove = r.GetInt();
			nodesToRemove.Log("Nodes to remove");
			for (int i = 0; i < nodesToRemove; i++)
			{
				GameManager.WorldManager.RemoveResourceNode(r.GetVector2Int());
			}
		}

	}
}