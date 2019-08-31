using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class levelCrop1
{
    public string[] crop = new string[3]{"Rose", "Sunflower", "Tulip"};
    public int levelID = 1;
}
public class levelCrop2{
    public string[] crop = new string[3]{"Corn", "Flour", "Wheat"};
    public int levelID = 2;
}
public class levelCrop3{
    public string[] crop = new string[6]{"Aubergine", "Lemonade", "Melon", "Cucumber", "Onion", "Tomato"};
    public int levelID = 3;
}
public class levelCrop4{
    public string[] crop = new string[4]{"Grape", "Orange", "Pear", "Strawberry"};
    public int levelID = 4;
}
public class CommonFunc{
        public static levelCrop1 crop1 = new levelCrop1();
        public static levelCrop2 crop2 = new levelCrop2();
        public static levelCrop3 crop3 = new levelCrop3();
        public static levelCrop4 crop4 = new levelCrop4();
    public int findCropLevel(string cropName){
        for (int i=0; i<crop1.crop.Length; i++){
            if (cropName == crop1.crop[i]){
                return crop1.levelID;
            }
        }
        for (int i=0; i<crop2.crop.Length; i++){
            if (cropName == crop2.crop[i]){
                return crop2.levelID;
            }
        }
        for (int i=0; i<crop3.crop.Length; i++){
            if (cropName == crop3.crop[i]){
                return crop3.levelID;
            }
        }
        for (int i=0; i<crop4.crop.Length; i++){
            if (cropName == crop4.crop[i]){
                return crop4.levelID;
            }
        }
        return 0;
    }
}
