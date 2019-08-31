using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Bounce back to inventorySlot
    Transform inventorySlot = null;
    [SerializeField] DropZone dropZone;
    [SerializeField] InventorySlot slot;

    public void OnBeginDrag(PointerEventData eventData)
    {        
        inventorySlot = this.transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;        
    }

    public void OnDrag(PointerEventData eventData)
    {        
        this.transform.position = eventData.position;        
    }

    public void OnEndDrag(PointerEventData eventData)
    {        
        if (!dropZone.isInDropZone())
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.transform.position = inventorySlot.transform.position;
        }
        else
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.transform.position = inventorySlot.transform.position;
            int gold = plusGold();
            gold = gold * slot.getStack();
            slot.plusGold(gold);
            slot.Delete();
        }
    }

    private int plusGold()
    {        
        if (slot.getName() == "Onion") // 1
        {
            return 8;
        }
        else if (slot.getName() == "Tomato") // 2
        {
            return 10;
        }
        else if (slot.getName() == "Cucumber") // 3
        {
            return 12;
        }
        else if (slot.getName() == "Corn") // 4
        {
            return 14;
        }
        else if (slot.getName() == "Orange") // 5
        {
            return 16;
        }
        else if (slot.getName() == "Flour") // 6
        {
            return 18;
        }
        else if (slot.getName() == "Melon") // 7
        {
            return 20;
        }
        else if (slot.getName() == "Lemonade") // 8
        {
            return 22;
        }
        else if (slot.getName() == "Aubergine") // 9
        {
            return 24;
        }
        else if (slot.getName() == "Wheat") // 10
        {
            return 26;
        }
        else if (slot.getName() == "Grape") // 11
        {
            return 28;
        }
        else if (slot.getName() == "Strawberry") //12
        {
            return 30;
        }       
        else if (slot.getName() == "Tulip") //13
        {
            return 32;
        }
        else if (slot.getName() == "Sunflower") //14
        {
            return 34;
        }
        else if (slot.getName() == "Pear") //15
        {
            return 36;
        }
        else if (slot.getName() == "Rose") //16
        {
            return 40;
        }
        else
        {
            return 0;
        }
    }
}
