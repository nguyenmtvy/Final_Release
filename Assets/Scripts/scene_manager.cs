using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

public class scene_manager : MonoBehaviour
{
    bool menuActivate;
    bool level1, level2, level3, level4;
    int gold;
    character player;
    public Text countdownText;
    public GameObject canvas;
    float reswanDelay;
    public manager mground_manager;  
    public AudioController audioController;
    public Inventory inventory;
    GameObject row1;
    GameObject row2;
    GameObject row3;
    GameObject row4;

    private void Awake() {
        row1 = this.gameObject.transform.GetChild(0).gameObject;
        row2 = this.gameObject.transform.GetChild(1).gameObject;
        row3 = this.gameObject.transform.GetChild(2).gameObject;
        row4 = this.gameObject.transform.GetChild(3).gameObject;
        level1 = false;
        level2 = false;
        level3 = false;
        level4 = false;
        audioController = FindObjectOfType<AudioController>();
        player = FindObjectOfType<character>();
        gold = player.gold;
        inventory = FindObjectOfType<Inventory>();
    }
    void Start()
    {
        load();
        menuActivate = false;
        //player = FindObjectOfType<character>();
        reswanDelay = 5.0f;
        canvas.SetActive(false); 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuActivate==false){
            //activate save_btn, save&quit_btn and save&back_btn
            menuActivate = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && menuActivate==true){
            //disable save_btn, save&quit_btn and save&back_btn
            menuActivate = false;
        }
        if (player.gold == 0 && level1 == false){
            getLevel1();
            level1 = true;
        }
        if (player.gold >= 100 && level2 == false){
            level2 = true;
            getLevel2();
        }
        if (player.gold >= 300 && level3 == false){
            level3 = true;
            getLevel3();
        }
        if (player.gold >= 900 && level4 == false){
            level4 = true;
            getLevel4();
        }
    }
    /*
        save load process
     */
    public void saveOnly(){
        SaveGround();
    }
    public void saveQuit(){
        saveOnly();
        Application.Quit();
    }
    public void load(){
        LoadGround();
    }
    void SaveGround(){
        try {
            pushData();
        } catch{
            Debug.Log("There is an issue while pushing data");
        }
        FileStream file = new FileStream (Application.persistentDataPath + "/savedGround.data", FileMode.OpenOrCreate);
        try {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, mground_manager);
        } catch{
            Debug.Log("There is an issue while serializing data");
        }
        file.Close();
    } 
    void LoadGround(){
        Debug.Log(Application.persistentDataPath + "/savedGround.data");
        try{
            FileStream file = new FileStream (Application.persistentDataPath + "/savedGround.data", FileMode.Open);
            try{
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                mground_manager = (manager) binaryFormatter.Deserialize(file);
                pullData();
            } catch{
                Debug.Log("Deserialize error");
            }
            file.Close();
        } catch{
            Debug.Log("load data failed");
            mground_manager.row1.rowStat = new ground_stat[8];
            mground_manager.row2.rowStat = new ground_stat[7];
            mground_manager.row3.rowStat = new ground_stat[8];
            mground_manager.row4.rowStat = new ground_stat[8];
            mground_manager.row1 = this.row1.GetComponent<farming_row>().stat;
            mground_manager.row2 = this.row2.GetComponent<farming_row>().stat;
            mground_manager.row3 = this.row3.GetComponent<farming_row>().stat; 
            mground_manager.row4 = this.row4.GetComponent<farming_row>().stat;
        }
    }

    /*
        navigate process
     */
    public void backToMenuScene(){
        saveOnly();
        //SceneManager.LoadScene("menu");
    }
    public void respawn(){
        StartCoroutine ("respawnRoutine");
    }
    public IEnumerator respawnRoutine (){
        canvas.SetActive(true);
        float timeLeft = reswanDelay;
        player.gameObject.SetActive(false);
        while (timeLeft > 0){
            yield return new WaitForSeconds(1.0f);
            countdownText.text = (timeLeft-1).ToString();
            timeLeft--;
        }

        player.transform.position = player.respawnPoint.transform.position;
        player.gameObject.SetActive(true);
        canvas.SetActive(false);
    }
    /*
        process ground data
     */
    public void pullData(){
        try {
            this.row1.GetComponent<farming_row>().stat = mground_manager.row1;
            this.row2.GetComponent<farming_row>().stat = mground_manager.row2;
            this.row3.GetComponent<farming_row>().stat = mground_manager.row3;
            this.row4.GetComponent<farming_row>().stat = mground_manager.row4;
            
            this.gold = mground_manager.gold;
            this.player.gold = this.gold;
            
            this.row1.GetComponent<farming_row>().pullData();
            this.row2.GetComponent<farming_row>().pullData();
            this.row3.GetComponent<farming_row>().pullData();
            this.row4.GetComponent<farming_row>().pullData();

            try {
                this.audioController.setting = mground_manager.setting;
                this.audioController.pullSetting();
            } catch{
                Debug.Log("audiocontroller pull data error");
            }
            try {
                this.player.audiosetting = mground_manager.setting;
                this.player.pullSetting();
            } catch{
                Debug.Log("player pull data error");
            }
            try {
                this.inventory.slotStat = mground_manager.inventoryStat;
                this.inventory.pullInventory();
            } catch{
                Debug.Log("inventory pull data error");
            }
        } catch {
            Debug.Log("scene_manager pull data error");
        }

    }
    public void pushData(){
        try {
            this.row1.GetComponent<farming_row>().pushData();
            this.row2.GetComponent<farming_row>().pushData();
            this.row3.GetComponent<farming_row>().pushData();
            this.row4.GetComponent<farming_row>().pushData();
            this.audioController.pushSetting();
            this.inventory.pushInventory();

            mground_manager.gold = this.gold;
            mground_manager.row1 = this.row1.GetComponent<farming_row>().stat;
            mground_manager.row2 = this.row2.GetComponent<farming_row>().stat;
            mground_manager.row3 = this.row3.GetComponent<farming_row>().stat;
            mground_manager.row4 = this.row4.GetComponent<farming_row>().stat;
            mground_manager.setting = this.audioController.setting;
            mground_manager.inventoryStat = this.inventory.slotStat;
        } catch {
            Debug.Log("scene_manager push data error");
        }
    }
    void getLevel1(){
        levelCrop1 crop1 = new levelCrop1();
        for (int i=0; i<crop1.crop.Length; i++){
            GameObject ground = row1.transform.GetChild(i).gameObject;
            GameObject prefab = (GameObject) Resources.Load(crop1.crop[i]) as GameObject;
            GameObject newCrop = Instantiate(prefab, ground.transform.position, Quaternion.identity);
            newCrop.transform.SetParent(ground.transform);
            newCrop.GetComponent<crop0>().levelID = 1;
            //newCrop.GetComponent<crop0>().adoptTime = newCrop.GetComponent<crop0>().time;
            newCrop.GetComponent<crop0>().adoptTime = 0;
        }
    }
    void getLevel2(){
        levelCrop2 crop2 = new levelCrop2();
        for (int i=0; i<crop2.crop.Length; i++){
            GameObject ground = row1.transform.GetChild(i).gameObject;
            GameObject prefab = (GameObject) Resources.Load(crop2.crop[i]) as GameObject;
            GameObject newCrop = Instantiate(prefab, ground.transform.position, Quaternion.identity);
            newCrop.transform.SetParent(ground.transform);
            newCrop.GetComponent<crop0>().levelID = 2;
            newCrop.GetComponent<crop0>().adoptTime = 0;
        }
    }
    void getLevel3(){
        levelCrop3 crop3 = new levelCrop3();
        for (int i=0; i<crop3.crop.Length; i++){
            GameObject ground = row1.transform.GetChild(i).gameObject;
            GameObject prefab = (GameObject) Resources.Load(crop3.crop[i]) as GameObject;
            GameObject newCrop = Instantiate(prefab, ground.transform.position, Quaternion.identity);
            newCrop.transform.SetParent(ground.transform);
            newCrop.GetComponent<crop0>().levelID = 3;
            newCrop.GetComponent<crop0>().adoptTime = 0;
        }
    }
    void getLevel4(){
        levelCrop4 crop4 = new levelCrop4();
        for (int i=0; i<crop4.crop.Length; i++){
            GameObject ground = row1.transform.GetChild(i).gameObject;
            GameObject prefab = (GameObject) Resources.Load(crop4.crop[i]) as GameObject;
            GameObject newCrop = Instantiate(prefab, ground.transform.position, Quaternion.identity);
            newCrop.transform.SetParent(ground.transform);
            newCrop.GetComponent<crop0>().levelID = 4;
            newCrop.GetComponent<crop0>().adoptTime = 0;
        }
    }
}