using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
   
    [SerializeField] Camera cameraHolder;
    RaycastHit hit;

   // publuc string InteractText = "Press key"
    bool isContect = false;
    // Update is called once per frame


    void Update()
    {
        

        
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            CheckObject();        }
    }


    void CheckObject()
    {
        Vector3 MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        if(Physics.Raycast(cameraHolder.ScreenPointToRay(MousePos), out hit, 100))
        {
            if(hit.transform.CompareTag("Cactus"))
            {
                if(!isContect)
                {
                    isContect = true;
                    Debug.Log(hit.collider.name);
                }
                
            }
            else
            {
                if (isContect)
                {
                    isContect = false;
                }
            }
            
        }
    }
}
