using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Dictionary<Item,uint> itemDict;


    private void Awake()
    {
        itemDict = new Dictionary<Item, uint>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Item temp = new SkinItem();
        itemDict.Add(temp, 0);
        temp = new WaterItem();
        itemDict.Add(temp, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
