using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
	public static RoomManager Instance;

	void Awake()
	{
		if (Instance) //체크
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Instance = this;
	}

	public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		Vector3 pos= new Vector3(-6, 1, -7);
		if (scene.buildIndex == 1) // 게임씬
		{
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Etalo_body_AllMerge 1"), pos, Quaternion.identity);
		}
	}
}