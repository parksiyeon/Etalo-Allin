using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeBasic : MonoBehaviour
{
    //public Transform tranformCube;
    [SerializeField]
    private float walkSpeed;
    private float lookSensitive; // 카메라 감도
    private Rigidbody myRigid; 

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        move();
    }

    private void move()
    {
        float moveDirX = Input.GetAxis("Horizontal");
        float moveDirZ = Input.GetAxis("Vertical");
        Vector3 moveHorizontal = transform.right * moveDirX; //좌우 이동
        Vector3 moveVertical = transform.forward * moveDirZ; //앞뒤 이동

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * walkSpeed; // 속도

        myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
