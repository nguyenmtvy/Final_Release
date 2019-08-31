using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemPrice;
    public Color bgColor;
}
