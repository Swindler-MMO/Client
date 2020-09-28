using System;
using LiteNetLib;
using Multiplayer.Packets;
using Swindler.Game;
using Swindler.Utils;
using UnityEngine;

namespace Swindler.Player.Authoritative.Movement
{
	public class SendPlayerPosition : MonoBehaviour
	{
		private const float SEND_THRESHOLD = 0.0001f;
		private const float UPDATE_TIME = 1f; // send update every 1000ms

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
			return (lastPosition - transform.position).sqrMagnitude > SEND_THRESHOLD && lastUpdate >= UPDATE_TIME;
		}
	}
}