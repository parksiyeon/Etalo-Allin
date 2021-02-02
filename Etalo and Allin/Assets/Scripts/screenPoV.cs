using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// 에임 위치를 찾는 스크립트

public class screenPoV : MonoBehaviour
{

    
    private Vector3 ScreenCenterPoint;

    void Start()
    {
        ScreenCenterPoint = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
    }
}
