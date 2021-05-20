using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MousePointerOnImage : MonoBehaviour
{
    private EventTrigger eventTrigger;
    public Image detailImagePrefab;
    private bool isShowDetailImage = false;
    private Image instantinatedObject = null;


    // Start is called before the first frame update
    void Start()
    {
        eventTrigger = gameObject.AddComponent<EventTrigger>();


        EventTrigger.Entry mousePointerEnter = new EventTrigger.Entry();
        mousePointerEnter.eventID = EventTriggerType.PointerEnter;
        mousePointerEnter.callback.AddListener((data) => MousePointerEnter((PointerEventData)data, gameObject));
        eventTrigger.triggers.Add(mousePointerEnter);

        EventTrigger.Entry mousePointerExit = new EventTrigger.Entry();
        mousePointerExit.eventID = EventTriggerType.PointerExit;
        mousePointerExit.callback.AddListener((data) => MousePointerExit((PointerEventData)data, gameObject));
        eventTrigger.triggers.Add(mousePointerExit);


    


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void MousePointerEnter(PointerEventData data,GameObject g)
    {
        Debug.Log(gameObject.name + "입장");
       
        instantinatedObject = Instantiate(detailImagePrefab, transform.parent.parent);
        instantinatedObject.GetComponent<RectTransform>().position = gameObject.transform.position;

       

        isShowDetailImage = true;
       
    }

    private void MousePointerExit(PointerEventData data, GameObject g)
    {
        Debug.Log(gameObject.name + "퇴장");
        
        Destroy(instantinatedObject.gameObject);
        isShowDetailImage = false;
        

    }



}
