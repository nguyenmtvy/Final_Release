using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor;


public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public int maxStack = 20;
    [SerializeField] ImageButton imageButton;
    [SerializeField] GameObject removeButton;
    [SerializeField] TextMeshProUGUI stack;    
    [SerializeField] character player;
    public inventory_slot slotStat;

    private Inventory inventory;
    public string name;
    public int currentStack;
    private Button btnX;
    public bool isDeleted; //empty = false
    private float cropTime;
    // Start is called before the first frame update
    // Manage icons and button click on this slot
    private void Awake()
    {
        this.name = null;
        this.currentStack = 0;
        isDeleted = false;
        inventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>();
        player = FindObjectOfType<character>();
        btnX = removeButton.GetComponent<Button>();        
    }
    private void Update()
    {
        if(currentStack == 0) { Delete(); }
        else { Interactable(true); }
    }
    public void plusGold(int money)
    {
        player.gold += money;
        player.goldStr.SetText(player.gold.ToString());
        Debug.Log("gold: " + player.gold);
    }

    public void setCropTime(float time)
    {
        cropTime = time;
    }
    public void setSprite(Sprite sprite)
    {
        imageButton.setIcon(sprite);
    }

    public Sprite getSprite()
    {
        return imageButton.getIcon();
    }

    public void setName(string newName)
    {
        name = newName;
        isDeleted = false;
    }

    public string getName()
    {
        return name; 
    }

    public void setStack(string tmp)
    {
        stack.SetText(tmp);
        stack.enabled = true;
    }

    public int getStack()
    {
        return currentStack;
    }

    public void increaseStack()
    {
        currentStack++;
    }

    public void Interactable(bool option)
    {
        btnX.interactable = option;
    }

    public void Delete()
    {
        isDeleted = true;
        name = null;
        currentStack = 0;
        stack.SetText("0");
        stack.enabled = false;
        btnX.interactable = false;
        imageButton.resetSprite();
    }

    public bool ifDeleted()
    {
        return isDeleted;
    }

    public void SetisDeleted(bool option)
    {
        isDeleted = option;
    }

    public void useItem()
    {
        if (currentStack > 0){
            try{
                GameObject prefab = (GameObject) Resources.Load(name) as GameObject;
                GameObject currentGround = player.mfarming_ground;

                if (currentGround != null && currentGround.GetComponent<farming_ground>().empty == true){
                    GameObject newCrop =
                    Instantiate(prefab, currentGround.transform.position, Quaternion.identity);
                    newCrop.transform.SetParent(currentGround.transform);
                    currentStack--;                
                    setStack(currentStack.ToString()); 
                    currentGround.GetComponent<farming_ground>().empty = false;
                    newCrop.GetComponent<crop0>().adoptTime = newCrop.GetComponent<crop0>().time;
                }
            } catch{
                Debug.Log("InventorySlot cannot find needed GameObject");
            }  
        }
    }

    //ToolTip
    public void OnPointerExit(PointerEventData eventData)
    {
        //Hide tooltip
        inventory.HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {        
        //Show tooltip
        if (!isDeleted)
        {
            int price = GetPrice(name);
            string tooltip_str = name + '\n' + '$' + price.ToString() + "\nTime: " + cropTime.ToString() + 's';
            inventory.SetTooltip(tooltip_str);
            inventory.ShowTooltip(this.transform.position);
        }
        
    }

    public int GetPrice(string name)
    {
        if (name == "Onion") // 1
        {
            return 8;
        }
        else if (name == "Tomato") // 2
        {
            return 10;
        }
        else if (name == "Cucumber") // 3
        {
            return 12;
        }
        else if (name == "Corn") // 4
        {
            return 14;
        }
        else if (name == "Orange") // 5
        {
            return 16;
        }
        else if (name == "Flour") // 6
        {
            return 18;
        }
        else if (name == "Melon") // 7
        {
            return 20;
        }
        else if (name == "Lemonade") // 8
        {
            return 22;
        }
        else if (name == "Aubergine") // 9
        {
            return 24;
        }
        else if (name == "Wheat") // 10
        {
            return 26;
        }
        else if (name == "Grape") // 11
        {
            return 28;
        }
        else if (name == "Strawberry") //12
        {
            return 30;
        }
        else if (name == "Tulip") //13
        {
            return 32;
        }
        else if (name == "Sunflower") //14
        {
            return 34;
        }
        else if (name == "Pear") //15
        {
            return 36;
        }
        else if (name == "Rose") //16
        {
            return 40;
        }
        else
        {
            return 0;
        }
    }
    public void pullData(){
        try {
            setName(this.slotStat.cropName);
            SetisDeleted(this.slotStat.isDeleted);
            setStack(slotStat.currentStack.ToString());
            this.currentStack = slotStat.currentStack;
            //Debug.Log(this.name);

            //load sprite
            if (this.slotStat.cropName != null){
                Debug.Log(this.name);
                GameObject prefab = (GameObject) Resources.Load(name) as GameObject;
                setSprite(prefab.GetComponent<PickUp>().shopIcon);
                cropTime = prefab.GetComponent<crop0>().time;
            }
            
        } catch{
            Debug.Log("inventory slot pull errror");
        }
    }
    public void pushData(){
        try{
            slotStat.currentStack = getStack();
            slotStat.cropName = getName();
            slotStat.isDeleted = this.isDeleted;
        } catch{
            Debug.Log("inventory slot push errror");
        }
    }
}
