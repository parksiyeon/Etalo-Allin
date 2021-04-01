using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;
	GameObject AllinPrfab;
	GameObject EtaloPrefab;



	void Awake()
	{

		
		PV = GetComponent<PhotonView>();
	}

	void Start()
	{
		
		if (PV.IsMine)//내 포톤 네트워크이면
		{
			CreateController();
		}
	}


    void CreateController()
	{
		//GameObject etalo = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Etalo_2"), Vector3.zero, Quaternion.identity);
		//GameObject allin = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Allin_2"), Vector3.zero, Quaternion.identity);
	//	PhotonNetwork.Instantiate(AllinPrfab.name,Vector3.zero, Quaternion.identity);

		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Etalo_2"), Vector3.zero, Quaternion.identity);

	}

	/*	public void Die()
		{
			PhotonNetwork.Destroy(controller);
			CreateController();
		}*/
}