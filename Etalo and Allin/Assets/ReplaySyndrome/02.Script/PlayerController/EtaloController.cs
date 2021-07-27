using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class EtaloController : MonoBehaviourPunCallbacks
{
    PhotonView PV;

    // Compononts
    #region
    private CharacterController cc;
    private Animator animator;
    #endregion

    // GameObjects
    #region
    public GameObject inventoryUI;
    public GameObject composeUI;
    public GameObject aimUI;
    public GameObject cameraArm;
    public GameObject fieldInteractableObjectItemName;

    public GameObject placeObject;
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

    public float maxHP = 100;
    public float currHP;    
    public float maxHungry = 100f;
    public float currHungry;
    public float maxThirst = 100;
    public float currThirst;
    public double optimalTemperature = 36.5;
    public double currTemperature;
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
    public bool itemAssembleState = false;
    #endregion



    EtaloController()
    {
    }

    void Awake()
    {
 
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(animator);
            Destroy(GetComponentInChildren<Camera>().gameObject);
        
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

        {
            return;
        }

        CharacterMove();
        SetAnimatorParameter();
        MouseInput();
        InterAction();
        CameraSetting();
        CharacterInfoSetting();
       // print(cc.isGrounded);
    }

    void CharacterMove()
    {
        XAxis = Input.GetAxis("Horizontal");
        ZAxis = Input.GetAxis("Vertical");

        
        
        RaycastHit hit;

        Debug.DrawRay(transform.position, Vector3.down,Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, 0.3f))
        {

            ySpeed = 0;
            if (Input.GetButtonDown("Jump"))
            {
                
                animator.SetTrigger("Jump");
                ySpeed = 10;
            }

            animator.SetBool("IsGrounded", true);
        }
        else
        {
            
            animator.SetBool("IsGrounded", false);
        }
        


        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * rotSpeed * mouseX);

        cc.Move(transform.TransformDirection(new Vector3(XAxis, 0, ZAxis).normalized * Time.deltaTime * speed));
        cc.Move(transform.TransformDirection(new Vector3(0, 1, 0).normalized * ySpeed * Time.deltaTime));


        ySpeed -= Time.deltaTime * gravity;
    }

    void SetAnimatorParameter()
    {
        //animator.SetFloat(animatorParameterXAxis, XAxis);
        //animator.SetFloat(animatorParameterZAxis, ZAxis);
    }

    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (placeObjectGizmo != null && itemAssembleState)
            {
                Instantiate(placeObjectGizmo);
                itemAssembleState = false;
                aimUI.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
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
                if(groundItem)
                {
                    
                    animator.SetTrigger(groundItem.animatorTrigger);
                    GetComponent<Inventory>().AddItem(groundItem.item);
                    animator.SetTrigger(groundItem.animatorTrigger);

                    print(groundItem.item.itemName);
                    Destroy(hit.collider.gameObject);
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

    void InteractableObjectIdentifier()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == 10)
            {
                if (hit.collider.gameObject != highlightObject && highlightObject != null)
                {
                    //highlightObject.GetComponent<Renderer>().material.color = Color.gray;
                    //highlightObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    
                }

               
                fieldInteractableObjectItemName.SetActive(true);
                
                fieldInteractableObjectItemName.GetComponent<Text>().text = hit.collider.GetComponent<OnGroundItem>().item.itemName;
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

                if (fieldInteractableObjectItemName != null)
                {
                    fieldInteractableObjectItemName.SetActive(false);
                }
            }
            else
            {
                if (fieldInteractableObjectItemName == null)
                {
                    return;
                }

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
}
