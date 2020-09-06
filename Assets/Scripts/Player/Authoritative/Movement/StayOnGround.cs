using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Swindler.Player.Authoritative.Movement
{
	[RequireComponent(typeof(MoveVelocity))]
	class StayOnGround : MonoBehaviour
	{

		public Tilemap island;
		MoveVelocity moveVelocity;

		private void Start()
		{
			moveVelocity = GetComponent<MoveVelocity>();
		}

		private void Update()
		{

			var tile = island.GetTile(island.WorldToCell(transform.position));
			moveVelocity.SetIsOnGround(tile != null);

		}

	}
}
