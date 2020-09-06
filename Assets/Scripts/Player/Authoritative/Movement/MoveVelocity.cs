using Swindler.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Swindler.Player.Authoritative.Movement
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class MoveVelocity : MonoBehaviour
	{

		[SerializeField] private float moveSpeed;
		private Rigidbody2D rb;
		private Vector3 velocity;
		private Vector3 lastPostion = Vector3.zero;
		public bool isOnGround;

		public void SetVelocity(Vector3 velocity)
		{
			this.velocity = velocity;
		}

		public void SetIsOnGround(bool isOnGround)
		{
			this.isOnGround = isOnGround;
		}

		private void Update()
		{
			if (isOnGround)
				lastPostion = transform.position;
			else
				transform.position = lastPostion;
		}

		private void FixedUpdate()
		{
			int canMove = (isOnGround ? 1 : 0);
			rb.velocity = velocity * moveSpeed * canMove;
			rb.angularVelocity *= canMove;

			//TODO: Play animation here
		}

		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

	}
}
