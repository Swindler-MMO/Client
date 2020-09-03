using Swindler.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveVelocity : MonoBehaviour
{

	[SerializeField] private float moveSpeed;
	private Rigidbody2D rb;
	private Vector3 velocity;

	public void SetVelocity(Vector3 velocity)
	{
		this.velocity = velocity;
	}

	private void FixedUpdate()
	{
		rb.velocity = velocity * moveSpeed;

		//TODO: Play animation here
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

}
