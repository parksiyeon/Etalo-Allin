using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
	PlayerController playerController;

	void Awake()
	{
		playerController = GetComponentInParent<PlayerController>();
		
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(playerController.gameObject);
		if (collision.gameObject == playerController.gameObject)
			return;

		playerController.SetGroundedState(true);
	}

	void OnCollisioExit(Collision collision)
	{
		Debug.Log(playerController.gameObject);
		if (collision.gameObject == playerController.gameObject)
			return;

		playerController.SetGroundedState(false);
	}

	void OnCollisioStay(Collision collision)
	{
		Debug.Log(playerController.gameObject);
		if (collision.gameObject == playerController.gameObject)
			return;

		playerController.SetGroundedState(true);
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject == playerController.gameObject)
			return;

		playerController.SetGroundedState(true);
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == playerController.gameObject)
			return;

		playerController.SetGroundedState(false);
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == playerController.gameObject)
			return;

		playerController.SetGroundedState(true);
	}
}