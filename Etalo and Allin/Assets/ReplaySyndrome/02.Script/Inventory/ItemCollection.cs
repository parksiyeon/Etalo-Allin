using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public Item water;
    public Item wood;
    public Item stone;
    public Item composedTemp;

    private Dictionary<string, Item> composedItemDict;

    private void Awake()
    {
        composedItemDict = new Dictionary<string, Item>();
        composedItemDict.Add(stone.itemName + water.itemName + water.itemName, composedTemp);
        print(stone.itemName + water.itemName + water.itemName);
    }


    public Item ReturnComposedItem(string items)
    {
        if (composedItemDict.ContainsKey(items))
        {
            return composedItemDict[items];
        }

        print("없습니다.");
        return null;
    }


}
