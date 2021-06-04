﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public Item bone;
    public Item bonfire;
    public Item branch;
    public Item cactus;
    public Item cactusfruit;
    public Item candy;
    public Item fruit;
    public Item grilledmeat;
    public Item gun;
    public Item hatchet;
    public Item log;
    public Item meat;
    public Item pebble;
    public Item petal;
    public Item pickax;
    public Item poionjelly;
    public Item poisonsac;
    public Item rock;
    public Item rope;
    public Item sand;
    public Item shovel;
    public Item skin;  
    public Item slime;
    public Item slingshot;
    public Item soup;
    public Item tent;
    public Item thread;
    public Item water;
    public Item waterbag;

    private Dictionary<string, Item> composedItemDict;

    private void Awake()
    {
        composedItemDict = new Dictionary<string, Item>();

        composedItemDict.Add(branch.itemName + branch.itemName + branch.itemName + branch.itemName + branch.itemName + branch.itemName + branch.itemName +
            rock.itemName + rock.itemName + rock.itemName + rock.itemName + rock.itemName, bonfire); // 1.	모닥불 – 나뭇가지(7) + 돌(5)
        composedItemDict.Add(skin.itemName + skin.itemName +
            water.itemName + water.itemName + water.itemName + water.itemName + water.itemName +
            water.itemName + water.itemName + water.itemName + water.itemName + water.itemName,waterbag); // 3. 물가방 - 가죽(2) + 물(10)

        composedItemDict.Add(petal.itemName + petal.itemName + slime.itemName, candy); //캔디 - 꽃잎(2) + 점액(1)

        composedItemDict.Add(branch.itemName + meat.itemName + meat.itemName, grilledmeat); // 고기구이 - 나뭇가지(1) + 고기(2)

        
        composedItemDict.Add(meat.itemName + slime.itemName + slime.itemName, soup); // 수프 - 고기(1) + 점액(2)
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
