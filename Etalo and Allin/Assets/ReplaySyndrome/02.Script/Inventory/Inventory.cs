using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct InventoryBox
{
    public Item item;
    int count;
    public int Count { get { return count; } }
    public InventoryBox(Item i,int c)
    {
        item = i;
        count = 1;
    }

    public void AddCount()
    {
        
        count += 1;

    }
}


public class Inventory : MonoBehaviour
{
    
    public List<InventoryBox> itemList;
    public Image inventoryBoxPrefab;
    public GameObject ContentScreen;
    [SerializeField]
    private ItemCollection itemCollection;


    private void Awake()
    {
        itemList = new List<InventoryBox>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AddItem(itemCollection.water);
        AddItem(itemCollection.wood);
        AddItem(itemCollection.water);
        AddItem(itemCollection.wood);
        AddItem(itemCollection.water);
        AddItem(itemCollection.wood);
        AddItem(itemCollection.stone);
        AddItem(itemCollection.stone);
        AddItem(itemCollection.water);
        AddItem(itemCollection.wood);
        AddItem(itemCollection.water);
        AddItem(itemCollection.stone);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item)
    {

        int findedindex = itemList.FindIndex(x => x.item.ItemName == item.ItemName);

        if (findedindex != -1) // 찾기 성공 --- 찾기 실패시 -1을 리턴합니다. 근데왜이렇게해야값이증가하지
        {
            InventoryBox temp = itemList[findedindex];
            temp.AddCount();
            itemList[findedindex] = temp;
        }
        else
        {
            InventoryBox temp = new InventoryBox(item, 1);
            itemList.Add(temp);
        }
    }

    
}
