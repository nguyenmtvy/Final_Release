using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class farming_ground : MonoBehaviour
{
    GameObject dirt;
    public ground_stat stat;
    public bool empty;
    public bool dirtActive;
    public string cropName;
    public float timeLeft;
    static CommonFunc common;
    // Start is called before the first frame update
    private void Awake() {
        try {
            
            dirt = this.gameObject.transform.GetChild(2).gameObject;
            dirtActive = false;
            empty = false;
            cropName = null;
            timeLeft = 0;
            common = new CommonFunc();
        } catch{
            Debug.Log("There is an error occured while initializing");
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update(){
            dirt.SetActive(dirtActive);
    }
        
    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject.tag =="character" && Input.GetKeyDown(KeyCode.Space)){
            if (dirtActive == false) {
                dirt.SetActive(true);
                dirtActive = true;
                empty = true;
            }
        }
        if (col.gameObject.tag =="character" && Input.GetKeyDown(KeyCode.Tab) && this.empty == true){
            if (dirtActive == true){
                dirt.SetActive(false);
                dirtActive = false;
                empty = false;
            }
        }
        if (col.gameObject.tag == "character" && Input.GetKeyDown(KeyCode.Tab))
        {
            if (dirtActive == true)
            {
                dirt.SetActive(false);
                dirtActive = false;
                empty = false;
            }
        }
        
    }
    public void pushData(){
        try{
            try {
                crop0 cropStat = GetComponentInChildren<crop0>();
                PickUp pickUp = GetComponentInChildren<PickUp>();
                this.timeLeft = cropStat.timeLeft;
                this.cropName = pickUp.cropName;
            } catch{
                Debug.Log("there is no crop on ground: " + gameObject);
                this.timeLeft = 0;
                this.cropName = "";
            }

            this.stat.dirtActive = this.dirtActive;
            this.stat.empty = this.empty;
            this.stat.cropName = this.cropName;
            this.stat.timeLeft = this.timeLeft;
        } catch{
            Debug.Log("farming_ground push data error");
        }
    }
    public void pullData(){
        try{
            //Debug.Log(gameObject.name);
            this.cropName = this.stat.cropName;
            this.dirtActive = this.stat.dirtActive;
            this.empty = this.stat.empty;
            this.timeLeft = this.stat.timeLeft;

            if (this.cropName != ""){
                GameObject prefab = (GameObject) Resources.Load(name);
                GameObject newCrop = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
                newCrop.transform.SetParent(gameObject.transform);
                // newCrop.GetComponent<crop0>().adoptTime = newCrop.GetComponent<crop0>().time;
                newCrop.GetComponent<crop0>().levelID = common.findCropLevel(cropName);
                newCrop.GetComponent<crop0>().adoptTime = 0;
            }
        } catch{
            Debug.Log("farming_ground pull data error");
        }
    }
}
