using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallAddComposedItemFunc : MonoBehaviour
{
    public Item item = null;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Button>().onClick.AddListener(CallAddComposedItemInParent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        item = null;
    }

    public void CallAddComposedItemInParent()
    {
        if (item != null)
        {
            GetComponentInParent<ItemComposeUI>().AddItemInComposedItemView(item);
        }
    }
}
