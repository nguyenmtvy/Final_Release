using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;    
    private SpriteRenderer spriteRenderer;    
    [SerializeField] public Sprite shopIcon;

    private int stack = 1;
    private crop0 cropInfo;
    public string cropName;

    private void Awake()
    {
        cropName = this.tag;
        inventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>();                
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("character") && GetComponent<crop0>().cropAvailable)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {            
                if(inventory.slots[i].getName() == cropName)
                {
                    if (inventory.slots[i].getStack() < inventory.slots[i].maxStack)
                    {
                        
                        inventory.slots[i].increaseStack();
                        inventory.slots[i].increaseStack();
                        stack = inventory.slots[i].getStack();
                        inventory.slots[i].setStack(stack.ToString());                        
                        Destroy(gameObject);
                        break;
                    }
                }
                if (!inventory.isFull[i] && inventory.slots[i].ifDeleted())
                {
                    Debug.Log("collide with character");
                    //item can be added
                    inventory.isFull[i] = true;

                    //set icon
                    inventory.slots[i].setSprite(shopIcon);

                    //set slot's name
                    inventory.slots[i].setName(cropName);

                    //set stack (to 2)
                    inventory.slots[i].increaseStack();
                    inventory.slots[i].increaseStack();

                    stack = inventory.slots[i].getStack();
                    inventory.slots[i].setStack(stack.ToString());

                    //is not empty
                    inventory.slots[i].SetisDeleted(false);

                    //X button enable
                    inventory.slots[i].Interactable(true);

                    //set Time
                    cropInfo = this.gameObject.GetComponent<crop0>();
                    inventory.slots[i].setCropTime(cropInfo.time);

                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
