using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class EtaloController : MonoBehaviourPunCallbacks
{


    // Compononts
    #region
    private CharacterController cc;
    private Animator animator;
    private Inventory inventory;
    #endregion

    protected string animatorParameterXAxis = "XAxis";  //흠..
    protected string animatorParameterZAxis = "ZAxis";

    // GameObjects
    #region
    public GameObject inventoryUI = null;
    public GameObject composeUI = null;
    public GameObject aimUI = null;
    public GameObject cameraArm;
    public GameObject fieldInteractableObjectItemName;

    public GameObject placeObject;
    public Camera myCamera;
    private GameObject placeObjectGizmo;
    #endregion

    //GameObject IsActive
    #region
    private bool inventoryUIIsActive = false;
    private bool composeUIIsActive = false;
    private bool aimUIIsActive = false;
    #endregion


    //Etalo 
    #region
    private float speed = 10f;
    private float rotSpeed = 5f;
    private float ySpeed = 0f;
    private float gravity = 9.8f;
    private float isgrondedDistance = 0.1f;
    private bool isJump = false;

    [HideInInspector]
    public float maxHP = 100;
    [HideInInspector]
    public float currHP;
    [HideInInspector]
    public float maxHungry = 100f;
    [HideInInspector]
    public float currHungry;
    [HideInInspector]
    public float maxThirst = 100;
    [HideInInspector]
    public float currThirst;
    [HideInInspector]
    public double optimalTemperature = 36.5;
    [HideInInspector]
    public double currTemperature;
    [HideInInspector]
    public double dangerTemperatureAmount = 3.5;
    #endregion

    //InputValus
    #region
    private float XAxis = 0;
    private float ZAxis = 0;
    #endregion

    //Highlight Object
    GameObject highlightObject = null;

    //Character State
    #region
    [HideInInspector]
    public bool itemAssembleState = false;
    #endregion

    //AnimatorParamter
    AnimatorStateInfo currAnimatorStateInfo;

    private string axStateTag = "AxState";
    private string idleTag = "Idle";
    private string gunStateTag = "GunState";

    //Server
    PhotonView PV;

    EtaloController()
    {
    }

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();

        Canvas mainCanvas = GameObject.FindObjectOfType<Canvas>();

        aimUI = mainCanvas.transform.Find("AimUI").gameObject;
        inventoryUI = mainCanvas.transform.Find("InventoryUI").gameObject;
        composeUI = mainCanvas.transform.Find("ComposeUI").gameObject;
        fieldInteractableObjectItemName = mainCanvas.transform.Find("FieldInteractableItemName").gameObject;
        fieldInteractableObjectItemName.gameObject.SetActive(false);

        myCamera = cameraArm.GetComponentInChildren<Camera>();
        if(myCamera.GetComponent<ShakeCamera>() == null)
        {
            myCamera.gameObject.AddComponent<ShakeCamera>();
            print("ShakeCamera붙임");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)

        {
            // EquipItem(0);
        }

        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            //Destroy(animator);
            //Destroy(cc);
            Destroy(inventory);
          
        }

        Cursor.lockState = CursorLockMode.Locked;

        currHP = maxHP;
        currTemperature = optimalTemperature;
        currHungry = maxHungry;
        currThirst = maxThirst;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)

        {
            return;
        }


        AnimatorStateReset();
        CharacterMove();
        SetAnimatorParameter();
        MouseInput();
        InterAction();
        CameraSetting();
        CharacterInfoSetting();
       
    }

    void AnimatorStateReset()
    {
        currAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    void CharacterMove()
    {
        XAxis = Input.GetAxis("Horizontal");
        ZAxis = Input.GetAxis("Vertical");

        
        

        Debug.DrawRay(transform.position, Vector3.down,Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {

            if(ySpeed < 0)
            {
                isJump = false;               
                ySpeed = 0;
            }
          
            
            if (Input.GetButtonDown("Jump"))
            {
                print("바닥인식중");
                if (Physics.Raycast(transform.position, Vector3.down, 0.3f ))
                {
                    
                    if (!isJump)
                    {
                        Debug.Log("Jump");
                        isJump = true;
                        animator.SetTrigger("Jump");
                        ySpeed = 10;
                    }
                }
            }

            animator.SetBool("IsGrounded", true);
        }
        else
        {
            
            animator.SetBool("IsGrounded", false);
        }
        


        float mouseX = Input.GetAxis("Mouse X");
       
        if (!currAnimatorStateInfo.IsTag(axStateTag))
        {
            if (!inventoryUIIsActive && !composeUIIsActive)
            {
                transform.Rotate(Vector3.up * rotSpeed * mouseX);
            }
            
            cc.Move(transform.TransformDirection(new Vector3(XAxis, 0, ZAxis).normalized * Time.deltaTime * speed));
        }

        cc.Move(transform.TransformDirection(new Vector3(0, 1, 0).normalized * ySpeed * Time.deltaTime));


        if (!isJump)
        {
            cc.Move(transform.TransformDirection(new Vector3(0f, -0.001f, 0f))); //바닥에 붙이기
        }


        ySpeed -= Time.deltaTime * gravity;
    }

    void SetAnimatorParameter()
    {
        animator.SetFloat(animatorParameterXAxis, XAxis);
        animator.SetFloat(animatorParameterZAxis, ZAxis);
    }

    void MouseInput()
    {
        

        if (currAnimatorStateInfo.IsTag(idleTag)) // Idle State
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (placeObjectGizmo != null && itemAssembleState)
                {


                    //GameObject A = placeObjectGizmo;
                    Instantiate(placeObjectGizmo);
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "BornFireObject"), placeObjectGizmo.transform.position, placeObjectGizmo.transform.rotation);


                    itemAssembleState = false;
                    aimUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }

            if (Input.GetMouseButton(1))
            {
                animator.SetBool("GunState", true);
            }
        }
        else if (currAnimatorStateInfo.IsTag(gunStateTag))
        {
            if(Input.GetMouseButtonDown(0))
            {
                print("쏘자");
                var bullet = inventory.itemList.Find(x => x.item.itemName == "bullet");
                if (bullet.count > 0)
                {
                    inventory.MiusItem(bullet.item);
                    animator.SetTrigger("Shoot");
                }
            }

            if (!Input.GetMouseButton(1))
            {
                
                
                animator.SetBool("GunState", false);
                
            }            
        }
    }


    void InterAction()
    {
        
        InteractableObjectIdentifier();
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
            if (Physics.Raycast(ray, out hit))
            {
                var groundItem = hit.collider.gameObject.GetComponent<OnGroundItem>();
                if(groundItem && Vector3.Distance(transform.position, hit.collider.gameObject.transform.position) < 4)
                {
                    
                    animator.SetTrigger(groundItem.animatorTrigger);
                    GetComponent<Inventory>().AddItem(groundItem.item);

                    print(groundItem.item.itemName);

                    int obj_ID = hit.collider.gameObject.GetComponent<PhotonView>().ViewID; //오브젝트 ID를 찾는다.
                    PV.RPC("DestroyRPC", RpcTarget.AllBuffered, obj_ID);
                    //PhotonNetwork.Destroy(hit.collider.gameObject);
                    //PV.RPC("DestroyRPC", RpcTarget.AllBuffered,hit.collider);
                    //PhotonNetwork.Destroy(hit.collider.gameObject);
                    //Destroy(hit.collider.gameObject, groundItem.destroyTime);

                }
            }
        }

      

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUIIsActive)
            {
                inventoryUI.SetActive(false);
                aimUI.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                inventoryUIIsActive = false;
            }
            else if(!composeUIIsActive)
            {
                inventoryUI.SetActive(true);
                aimUI.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                inventoryUIIsActive = true;
            }
        }       
    

        if(Input.GetKeyDown(KeyCode.C))
        {
            if (composeUIIsActive)
            {
                composeUI.SetActive(false);
                aimUI.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                composeUIIsActive = false;
            }
            else if(!inventoryUIIsActive)
            {
               
                composeUI.SetActive(true);
                aimUI.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                composeUIIsActive = true;
            }
        }
    }


    [PunRPC]    //아이템 삭제하는 함수, 모든 클라이언트에게 삭제

    private void DestroyRPC(int obj_ID)
    {
        // PhotonNetwork.Destroy(PhotonView.Find(obj_ID).gameObject);
        print(obj_ID);
        Destroy(PhotonView.Find(obj_ID).gameObject);
    }

    void InteractableObjectIdentifier()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == 10 && Vector3.Distance(transform.position, hit.collider.gameObject.transform.position) < 4)
            {
                if (hit.collider.gameObject != highlightObject && highlightObject != null)
                {
                    //highlightObject.GetComponent<Renderer>().material.color = Color.gray;
                    //highlightObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    
                }

                
                fieldInteractableObjectItemName.SetActive(true);
                fieldInteractableObjectItemName.GetComponent<Text>().text = hit.collider.GetComponent<OnGroundItem>().item.itemName;
                //Debug.Log(hit.collider.GetComponent<OnGroundItem>().item.itemName);
                //Debug.Log("충돌했음");
                //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                //hit.collider.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                highlightObject = hit.collider.gameObject;


            }
            else if (hit.collider.gameObject.layer == 11)
            {
                if (itemAssembleState)
                {
                    if (placeObjectGizmo == null)
                    {
                     
                        
                        placeObjectGizmo = Instantiate(placeObject);
                        
                    }

                    placeObjectGizmo.SetActive(true);
                    placeObjectGizmo.transform.position = hit.point;
                    var angle = hit.normal;

                    placeObjectGizmo.transform.rotation = Quaternion.LookRotation(angle);
                    placeObjectGizmo.transform.Rotate(90, 0, 0);
                }

                if (highlightObject != null)
                {
                    //Debug.Log("충돌안했음");
                    //highlightObject.GetComponent<Renderer>().material.color = Color.gray;
                    highlightObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                }

                fieldInteractableObjectItemName.SetActive(false);
            }
            else
            {
               
                fieldInteractableObjectItemName.SetActive(false);
                
                if (placeObjectGizmo != null)
                {
                   
                    Destroy(placeObjectGizmo);
                    placeObjectGizmo = null;
                }


                if (highlightObject != null)
                {
                    //Debug.Log("충돌안했음");
                    //highlightObject.GetComponent<Renderer>().material.color = Color.gray;
                    highlightObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                }
            }
        }
        else
        {
            fieldInteractableObjectItemName.SetActive(false);
            Destroy(placeObjectGizmo);
            placeObjectGizmo = null;
            if (highlightObject != null)
            {
                //Debug.Log("충돌안했음");
                //highlightObject.GetComponent<Renderer>().material.color = Color.gray;
                highlightObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            }
        }

    }

    void CameraSetting()
    {
        if (!currAnimatorStateInfo.IsTag(axStateTag))
        {
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 camAngle = cameraArm.transform.rotation.eulerAngles;


            float resultCamYAngle = camAngle.x - mouseY;
            if (resultCamYAngle < 180f)
            {
                resultCamYAngle = Mathf.Clamp(resultCamYAngle, -1f, 70f);
            }
            else
            {
                resultCamYAngle = Mathf.Clamp(resultCamYAngle, 330, 361f);
            }

            if(!inventoryUIIsActive&&!composeUIIsActive)
            {
                cameraArm.transform.rotation = Quaternion.Euler(resultCamYAngle, camAngle.y, camAngle.z);
            }
            
        }
    
    }

    void CharacterInfoSetting()
    {
        currHP -= Time.deltaTime * 0.05f;
        currTemperature -= Time.deltaTime * 0.005;
        currHungry -= Time.deltaTime * 0.5f;
        currThirst -= Time.deltaTime * 0.5f;
    }


    public void SetMouseCorsorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetInventoryDisable()
    {
        inventoryUIIsActive = false;
    }

    public void SetComposeItemViewDisable()
    {
        composeUIIsActive = false;
    }

    public void UIReset()
    {
        inventoryUIIsActive = false;
        composeUIIsActive = false;
        aimUIIsActive = false;


        inventoryUI.SetActive(false);
        composeUI.SetActive(false);
        aimUI.SetActive(false);

        
    }

    public void OnWieldAx()
    {
        Debug.Log("OnWieldAx");
    }

    public void DamagedFromMonster(float damage)
    {
        currHP -= damage;
        print(currHP);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Camera.main.GetComponent<ShakeCamera>().StartShake();
        myCamera.GetComponent<ShakeCamera>().StartShake();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tornado")
        {
            Vector3 tornadoCenter = other.gameObject.transform.position;
            Vector3 playerCenter = transform.position;
            Vector3 directionToTornado = tornadoCenter - playerCenter;


            Vector3 tornadoCenterWithoutY = new Vector3(tornadoCenter.x, 0, tornadoCenter.z);
            Vector3 playerCenterWithoutY = new Vector3(playerCenter.x, 0, playerCenter.z);






            float distanceFromTornado = Vector3.Distance(tornadoCenterWithoutY, playerCenterWithoutY);
            cc.Move(directionToTornado.normalized * Time.deltaTime *
                (speed * (1 - distanceFromTornado / other.gameObject.GetComponent<Tornado>().tornadoRadius) * 1.2f));

            if ((1 - distanceFromTornado / other.gameObject.GetComponent<Tornado>().tornadoRadius) * 1.2f > 1)
            {
                myCamera.gameObject.SetActive(false);
            }
            print("빨려들어가는중");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myCamera.GetComponent<ShakeCamera>().StopShake();
    }
}
