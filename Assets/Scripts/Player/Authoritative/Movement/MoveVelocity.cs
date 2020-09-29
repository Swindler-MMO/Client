using System;
using Swindler.Utilities;
using System.Collections;
using System.Collections.Generic;
using Swindler.Utilities.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Swindler.Player.Authoritative.Movement
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class MoveVelocity : MonoBehaviour
	{

		[SerializeField] private float moveSpeed;
		private Rigidbody2D rb;
		public Vector3 velocity;
		public Vector3 lastPosition = Vector3.zero;
		public bool canMove;

		public void SetVelocity(Vector3 velocity)
		{
			this.velocity = velocity;
		}

		public void SetCanMove(bool canMove)
		{
			this.canMove = canMove;
		}

		private void Start()
		{
			SetLastPosition();
		}
		
		private void Update()
		{
			// if (canMove)
			// 	SetLastPosition();
			// else
			// 	transform.position = lastPosition;
		}

		private void SetLastPosition()
		{
			("Setting last position at canMove: " + canMove).Log();
			lastPosition = transform.position;
		}

		private void FixedUpdate()
		{
			// int canMove = (this.canMove ? 1 : 0);
			rb.velocity = moveSpeed * velocity;
			//rb.angularVelocity *= canMove;

			//TODO: Play animation here
		}

		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

	}
}
