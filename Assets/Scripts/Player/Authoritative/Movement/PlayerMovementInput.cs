using Swindler.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Swindler.Player.Authoritative.Movement
{
	[RequireComponent(typeof(MoveVelocity), typeof(StayOnGround))]
	public class PlayerMovementInput : MonoBehaviour
	{

		private MoveVelocity moveVelocity;

		private void Start()
		{
			moveVelocity = GetComponent<MoveVelocity>();
		}

		private void Update()
		{
			Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
			moveVelocity.SetVelocity(movement);
		}

	}
}
