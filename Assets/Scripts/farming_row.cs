using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class farming_row : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject farming_block1;
    GameObject farming_block2;
    GameObject farming_block3;
    GameObject farming_block4;
    GameObject farming_block5;
    GameObject farming_block6;
    GameObject farming_block7;
    GameObject farming_block8;
    public row_stat stat;
    farming_ground[] mRow;

    private void Awake() {
        try {
            this.farming_block1 = this.gameObject.transform.GetChild(0).gameObject;
            this.farming_block2 = this.gameObject.transform.GetChild(1).gameObject;
            this.farming_block3 = this.gameObject.transform.GetChild(2).gameObject; 
            this.farming_block4 = this.gameObject.transform.GetChild(3).gameObject;
            this.farming_block5 = this.gameObject.transform.GetChild(4).gameObject;
            this.farming_block6 = this.gameObject.transform.GetChild(5).gameObject;
            this.farming_block7 = this.gameObject.transform.GetChild(6).gameObject;
            if (gameObject.name == "ground_row1"){
                mRow = new farming_ground[7];
            }
            else {
                mRow = new farming_ground[8];
                this.farming_block8 = this.gameObject.transform.GetChild(7).gameObject;
            }
        } catch{
            Debug.Log("Cannot initialize ground of: " + this.gameObject.name);
        } finally{
            mRow[0] = farming_block1.GetComponent<farming_ground>();
            mRow[1] = farming_block2.GetComponent<farming_ground>();
            mRow[2] = farming_block3.GetComponent<farming_ground>();
            mRow[3] = farming_block4.GetComponent<farming_ground>();
            mRow[4] = farming_block5.GetComponent<farming_ground>();
            mRow[5] = farming_block6.GetComponent<farming_ground>();
            mRow[6] = farming_block7.GetComponent<farming_ground>();
            if (gameObject.name != "ground_row1"){
                mRow[7] = farming_block8.GetComponent<farming_ground>();
            }
            if (gameObject.name =="ground_row1"){
                stat.rowStat = new ground_stat[7];
            }
            else {
                stat.rowStat = new ground_stat[8];
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            mRow[0] = farming_block1.GetComponent<farming_ground>();
            mRow[1] = farming_block2.GetComponent<farming_ground>();
            mRow[2] = farming_block3.GetComponent<farming_ground>();
            mRow[3] = farming_block4.GetComponent<farming_ground>();
            mRow[4] = farming_block5.GetComponent<farming_ground>();
            mRow[5] = farming_block6.GetComponent<farming_ground>();
            mRow[6] = farming_block7.GetComponent<farming_ground>();
            if (gameObject.name != "ground_row1"){
                mRow[7] = farming_block8.GetComponent<farming_ground>();
            }
    }
    public void pushData(){
        try{
            for (int i=0; i<mRow.Length; i++){
                mRow[i].pushData();
                this.stat.rowStat[i].cropName = mRow[i].stat.cropName;
                this.stat.rowStat[i].dirtActive = mRow[i].stat.dirtActive;
                this.stat.rowStat[i].empty = mRow[i].stat.empty;
                this.stat.rowStat[i].timeLeft = mRow[i].stat.timeLeft;
            }
        } catch {
            Debug.Log("farming_row push data error");
        }
    }
    public void pullData(){
        try {
            //Debug.Log(gameObject.name);
            for (int i=0; i<mRow.Length; i++){
                mRow[i].stat = this.stat.rowStat[i];
                mRow[i].pullData();
            }
        } catch{
            Debug.Log("farming_row pull data error");
        }
    }
}
