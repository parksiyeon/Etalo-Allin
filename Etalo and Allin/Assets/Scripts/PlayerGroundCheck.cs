using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    PlayerController playerController;

    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
		Debug.Log(playerController.gameObject);
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == playerController.gameObject)
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



    void OnCollisionEnter(Collision collision)	//충돌 시 한번
    {

		if (collision.gameObject == playerController.gameObject)
				Debug.Log("enter");
		return;

        playerController.SetGroundedState(true);
	}

	void OnCollisionStay(Collision collision)   //충돌하는 동안 계속
	{
		if (collision.gameObject == playerController.gameObject)
			Debug.Log("stay");
		return;

		playerController.SetGroundedState(true);
	}
	void OnCollisionExit(Collision collision)	//충돌이 끝나면
    {
		if (collision.gameObject == playerController.gameObject)
			Debug.Log("exit");
		return;

		playerController.SetGroundedState(false);
	}


}
