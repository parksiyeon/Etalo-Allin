﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class EtaloController : AstronautController
{

    // Compononts
    #region
    private CharacterController cc;
    private Animator animator;
    #endregion

    // GameObjects
    #region
    public GameObject cameraArm;

    #endregion


    //Etalo 
    #region
    private float speed = 10f;
    private float rotSpeed = 5f;
    #endregion

    //InputValus
    #region
    private float XAxis = 0;
    private float ZAxis = 0;
    #endregion



    EtaloController()
    {
        print("에탈로 생성");
    }

    protected override void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;



    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
        SetAnimatorParameter();
        InterAction();
        CameraSetting();
    }

    void CharacterMove()
    {
        XAxis = Input.GetAxis("Horizontal");
        ZAxis = Input.GetAxis("Vertical");


        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * rotSpeed * mouseX);

        cc.Move(transform.TransformDirection(new Vector3(XAxis, 0, ZAxis) * Time.deltaTime * speed));
    }

    void SetAnimatorParameter()
    {
        animator.SetFloat(animatorParameterXAxis, XAxis);
        animator.SetFloat(animatorParameterZAxis, ZAxis);
    }


    void InterAction()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }

    void CameraSetting()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 camAngle = cameraArm.transform.rotation.eulerAngles;

        float resultCamYAngle = camAngle.x - mouseY;
        if (resultCamYAngle < 180f)
        {
            resultCamYAngle = Mathf.Clamp(resultCamYAngle, -1f, 45f);
        }
        else
        {
            resultCamYAngle = Mathf.Clamp(resultCamYAngle, 330f, 361f);
        }
        cameraArm.transform.rotation = Quaternion.Euler(resultCamYAngle, camAngle.y, camAngle.z);

    }
}
