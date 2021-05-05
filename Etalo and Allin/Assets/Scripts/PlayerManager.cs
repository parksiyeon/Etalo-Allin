using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
	
	PhotonView PV;

	


	void Awake()
	{

		Debug.Log("PlayerManager");
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
		Debug.Log("Create");
        Vector3 pos = new Vector3(-10, 2, -10);
		
		//GameObject etalo = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Etalo_2"), Vector3.zero, Quaternion.identity);
		//GameObject allin = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Allin_2"), Vector3.zero, Quaternion.identity);
		//	PhotonNetwork.Instantiate(AllinPrfab.name,Vector3.zero, Quaternion.identity);

		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "New_Etalo"), pos, Quaternion.identity);
		
		//Instantiate(찍어낼 오브젝트, 찍어낼 위치, 찍을때 회전값)
	}

	/*	public void Die()
		{
			PhotonNetwork.Destroy(controller);
			CreateController();
		}*/
}