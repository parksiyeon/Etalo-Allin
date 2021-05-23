using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemComposeUI : MonoBehaviour
{
    public GameObject composedView;
    public ItemCollection itemCollection;
    public GameObject inventoryView;
  
    private GameObject player;
    private bool[] composedArayyIsEquiped;
    private Button[] inventoryItemList;

    private ComposedItemView[] composedItemView;


    private void Awake()
    {
        composedItemView = composedView.GetComponentsInChildren<ComposedItemView>();


        inventoryItemList = inventoryView.GetComponentsInChildren<Button>();
        if (inventoryItemList == null)
        {
            Debug.Log("버튼못구함");
        }
        else
        {
            Debug.Log("버튼구함");
            Debug.Log(inventoryItemList[0].gameObject.name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        var items = p.GetComponent<Inventory>().itemList;

        if (inventoryItemList == null)
        {
            Debug.Log("버튼아직도못구함");
        }
        for (int i=0;i<items.Count;++i) // 아이템보이기
        {
            inventoryItemList[i].GetComponent<Image>().sprite = items[i].item.originalImage;
            inventoryItemList[i].GetComponentInChildren<Text>().text = items[i].Count.ToString();
        }
        for(int i=inventoryItemList.Length -1 ;i>items.Count -1 ;--i)
        {
            inventoryItemList[i].GetComponentInChildren<Text>().text = string.Empty;
        }

    }


}
