using Swindler.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveVelocity))]
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
		movement.Log("Movement velocity"); 
		moveVelocity.SetVelocity(movement);
	}

}
