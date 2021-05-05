using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventoryPanel;

    bool active = false;


    void OpenInven()
    {
        active = !active;
        inventoryPanel.SetActive(active);
    }
}
