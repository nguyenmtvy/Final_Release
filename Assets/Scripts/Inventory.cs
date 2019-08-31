using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public InventorySlot[] slots;
    public GameObject inventory;
    private TextMeshProUGUI tooltip_str;    
    private crop0 cropInfo;
    [SerializeField] GameObject tooltip;
    Vector3 posActive;
    Vector3 posDisable;
    RectTransform position; 
    public bool active;

    public inventory_stat slotStat;
    private void Awake() {
        this.slotStat.slots = new inventory_slot[20];
        posActive = new Vector3(-272f, -315.1f, 0f);
        posDisable = new Vector3(863f, -947f, 0f);
        position = inventory.GetComponent<RectTransform>();
        position.localPosition = posDisable;
        active = false;
    }


    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<slots.Length; i++){
            if (slots[i].currentStack == 20){
                isFull[i] = true;
            }
            else
            {
                isFull[i] = false;
            }
        }
        //Turn on and off inventory
        if (Input.GetButtonDown("Inventory"))
        {
            if (active == false){
                position.localPosition = posActive;
                active = true;
            }
            else {
                position.localPosition = posDisable;
                active = false;
            }
        }

        if (inventory.active == false)
        {
            tooltip.SetActive(false);
        }
    }

    public void ShowTooltip(Vector3 pos)
    {
        tooltip.SetActive(true);
        tooltip.transform.position = pos;

    }
    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    public void SetTooltip(string content)
    {
        tooltip_str = tooltip.GetComponentInChildren<TextMeshProUGUI>();
        tooltip_str.text = content;
    }


    public void pullInventory(){

        try{
            for (int i = 0; i < this.slots.Length; i++)
            {
                Debug.Log("In Setting Data");
                this.slots[i].slotStat.cropName = this.slotStat.slots[i].cropName;
                this.slots[i].slotStat.currentStack = this.slotStat.slots[i].currentStack;
                this.slots[i].slotStat.isDeleted = this.slotStat.slots[i].isDeleted;
                this.slots[i].pullData();
                Debug.Log("Finish In Setting Data");
            }
        } catch{
            Debug.Log("inventory pull error");
            
        }
    }
    public void pushInventory(){
        try{
            this.slotStat.slots = new inventory_slot[this.slots.Length];
            for (int i = 0; i < this.slots.Length; i++)
            {
                Debug.Log("pushing inventory");
                this.slots[i].pushData();
                this.slotStat.slots[i].cropName = this.slots[i].slotStat.cropName;
                this.slotStat.slots[i].currentStack = this.slots[i].slotStat.currentStack;
                this.slotStat.slots[i].isDeleted = this.slots[i].slotStat.isDeleted;
            }
        } catch{
            Debug.Log("inventory push error");
        }
    }
}
