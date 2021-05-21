using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Sprite originalImage;
    public Sprite detailImage;
    private string itemName;

    public string ItemName
    {
        get { return itemName;  }
    }

    
    public Item(string s)
    {
        itemName = s;
    }

    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemAction()
    {
        switch (itemName)
        {
            case "Water":
                Debug.Log("물마시자");
                break;

        }

    }


}
