using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventoryPanel;

    bool active = false;

    private void Start()
    {
        inventoryPanel.SetActive(active);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            active = !active;
            inventoryPanel.SetActive(active);
        }
    }
}
