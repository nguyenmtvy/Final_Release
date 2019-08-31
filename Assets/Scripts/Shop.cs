using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("List of items sold")]
    [SerializeField] private ShopItem[] shopItem;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private character player;
    [SerializeField] private TextMeshProUGUI floatingText;

    private crop0 cropTime;
    private Inventory inventory;
    private int stack = 1;

    private void Start()
    {
        PopulateShop();
        inventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>();
    }

    private void PopulateShop()
    {
        for (int i = 0; i < shopItem.Length; ++i)
        {
            ShopItem si = shopItem[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

            //ShopItem(Image, Button)
            // - Icon(Image)
            // - Price (TextMeshPro)
            // - Name (TextMeshPro)
            // - Time (TextMeshPro)

            //Change Icon
            itemObject.transform.GetChild(0).GetComponent<Image>().sprite = si.itemIcon;
            //Change Price
            itemObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = '$'+ si.itemPrice.ToString();
            //Change Name
            itemObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = si.itemName;
            //Change Time
            cropTime = GameObject.FindGameObjectWithTag(si.itemName).GetComponent<crop0>();
            itemObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = '/' + cropTime.time.ToString() + 's';
            //Grab button, assign function
            itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(si));

        }
    }

    public void ShopOnOff()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    private void OnButtonClick(ShopItem item)
    {
        if (player.gold >= item.itemPrice)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                //stack up
                if (inventory.slots[i].getName() == item.itemName)
                {
                    if (inventory.slots[i].getStack() < inventory.slots[i].maxStack)
                    {

                        inventory.slots[i].increaseStack();
                        stack = inventory.slots[i].getStack();
                        inventory.slots[i].setStack(stack.ToString());


                        //minus gold
                        player.gold -= item.itemPrice;
                        break;
                    }
                }
                //create new
                if (!inventory.isFull[i] && inventory.slots[i].ifDeleted())
                {
                    //item can be added
                    inventory.isFull[i] = true;

                    //set icon
                    inventory.slots[i].setSprite(item.itemIcon);

                    //set slot's name
                    inventory.slots[i].setName(item.itemName);

                    //set stack (to 1)
                    inventory.slots[i].increaseStack();
                    stack = inventory.slots[i].getStack();
                    inventory.slots[i].setStack(stack.ToString());

                    //slot is not empty anymore
                    inventory.slots[i].SetisDeleted(false);

                    //X button enable
                    inventory.slots[i].Interactable(true);

                    //minus gold
                    player.gold -= item.itemPrice;
                    break;
                }

            }
        }
        else
        {            
            Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        }
    }
}
