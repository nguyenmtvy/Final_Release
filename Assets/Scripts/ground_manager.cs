using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class manager
{
    public row_stat row1;
    public row_stat row2;
    public row_stat row3;
    public row_stat row4;
    public btn_setting setting;
    public inventory_stat inventoryStat;
    public int gold;
}

[System.Serializable]
public struct ground_stat{
    public bool empty;
    public bool dirtActive;
    public string cropName;
    public float timeLeft;
}

[System.Serializable]
public struct row_stat{
    public ground_stat[] rowStat;
}

[System.Serializable]
public struct btn_setting{
    public float volumeValue;
    public bool mute;
}

[System.Serializable]
public struct inventory_slot{
    public int currentStack;
    public string cropName;
    public bool isDeleted;
}

[System.Serializable]
public struct inventory_stat{
    public inventory_slot[] slots;
}