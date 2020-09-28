using Swindler.Multiplayer;
using UnityEngine;

namespace Player.Remote
{
	public class RemotePlayer : MonoBehaviour
	{

		public int id;
		public string playerName;

		public void SetNetPlayer(NetPlayer pl)
		{
			id = pl.Id;
			playerName = pl.Name;

			name = $"Remote Player #{pl.Id} ({pl.Name})";
			transform.position = pl.Position;
		}

	}
}