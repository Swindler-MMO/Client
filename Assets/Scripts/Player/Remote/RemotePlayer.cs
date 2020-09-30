﻿using Swindler.Multiplayer;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Player.Remote
{
	public class RemotePlayer : MonoBehaviour
	{

		public int id;
		public string playerName;

		private float lastUpdated;
		
		public void SetNetPlayer(NetPlayer pl)
		{
			id = pl.Id;
			playerName = pl.Name;

			name = $"Remote Player #{pl.Id} ({pl.Name})";
			transform.position = pl.Position;
		}

		public void SetPosition(Vector2 position)
		{
			//TODO: EXTRAPOLATION
			$"[Player #{id}] last updated {Time.time - lastUpdated} ms ago is at ({position.x};{position.y})".Log();
			transform.position = position;
			
			lastUpdated = Time.time;
		}

		public void OnDisconnect()
		{
			Destroy(gameObject);
		}
		
	}
}