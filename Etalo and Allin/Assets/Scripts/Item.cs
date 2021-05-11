using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject  // 게임 오브젝트에 붙일 필요 X 
{
    public enum ItemType 
    {
        Equipment,
        Used,
        Ingredient,
        ETC,
    }

    public string itemName; // 이름
    public ItemType itemType; // 유형
    public Sprite itemImage; // 이미지(인벤 토리 안에서 띄울)
    

   
}