using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;

	GameObject controller;

	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	void Start()
	{
		if (PV.IsMine)
		{
			CreateController();
		}
	}

	void CreateController()
	{
		
		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Etalo_body"), Vector3.zero, Quaternion.identity);
		

	}

	/*	public void Die()
		{
			PhotonNetwork.Destroy(controller);
			CreateController();
		}*/
}