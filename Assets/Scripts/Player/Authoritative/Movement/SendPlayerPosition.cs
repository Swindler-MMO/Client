using System;
using LiteNetLib;
using Multiplayer.Packets;
using Swindler.Game;
using Swindler.Utilities;
using UnityEngine;

namespace Swindler.Player.Authoritative.Movement
{
	public class SendPlayerPosition : MonoBehaviour
	{
		private const float MOVE_THRESHOLD = 0.0001f;
		private static readonly float UPDATE_TIME = Config.MovementUpdateTime;

		private Vector3 lastPosition;
		private float lastUpdate;

		private void Start()
		{
			lastPosition = transform.position;
			lastUpdate = 0f;

			SendPosition();
		}

		private void Update()
		{
			lastUpdate += Time.deltaTime;

			if (CanSend())
				SendPosition();
		}

		private void SendPosition()
		{
			lastPosition = transform.position;
			lastUpdate = 0f;
			GameManager.Server.Send(new PlayerPositionPacket(lastPosition), DeliveryMethod.Sequenced);
		}

		private bool CanSend()
		{
			return (lastPosition - transform.position).sqrMagnitude > MOVE_THRESHOLD && lastUpdate >= UPDATE_TIME;
		}
	}
}