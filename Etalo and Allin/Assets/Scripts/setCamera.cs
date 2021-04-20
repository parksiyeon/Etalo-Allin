using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCamera : MonoBehaviour
{
    private GameObject cameraTag;

    [SerializeField]
    Vector3 delta;

    [SerializeField]
    GameObject player;

    void Start()
    {
        cameraTag = GameObject.FindGameObjectWithTag("camera");        

        if (cameraTag != null)
        {
            Debug.Log(cameraTag.name + "find!");
        }
        else
        {
            Debug.Log("serch fail!");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + delta;
        transform.LookAt(player.transform);
    }
}
