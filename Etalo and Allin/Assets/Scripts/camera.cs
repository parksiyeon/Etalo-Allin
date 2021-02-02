using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode mode = Define.CameraMode.QuaterView; // Define 스크립트에 있는 enum 중 하나
    Vector3 delta;
    public Transform player;

    private Transform cameraTransform;


    void Start()
    {
        cameraTransform = GetComponent<Transform>();
            
    }

    void LateUpdate()
    {
        if (mode == Define.CameraMode.QuaterView)
        {
            cameraTransform.position = new Vector3(player.position.x - 0.52f, cameraTransform.position.y, player.position.z - 6.56f);
            //transform.position = player.transform.position + delta;
            transform.LookAt(player.transform);
        }
    }
}
