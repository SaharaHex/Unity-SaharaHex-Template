using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_InputField TextSaveName;
    public TMP_Text TextLoadData;

    private void Start()
    {
        LoadManager lMan = new LoadManager(); 
        DataPlaySettings MyPlayData = lMan.LoadPlaySettingsCurrent(); //if null it will create new game

        TextLoadData.text = MyPlayData.saveName + " - " + MyPlayData.health; //only for testing
    }

    public void SaveGameSlot(Text hidSlotNum)
    {
        string SaveName = "Save " + hidSlotNum.text;
        if (!string.IsNullOrEmpty(TextSaveName.text))
            SaveName = TextSaveName.text;

        LoadManager lMan = new LoadManager(); //load game
        DataPlaySettings MyPlayData = lMan.LoadPlaySettingsCurrent(); //need to load the Current Data.. if Current Data emtpty it's new game

        if (hidSlotNum.text == "1")
        {
            SaveManager sMan = new SaveManager(); //save game
            //to remove
            MyPlayData.health = 100;
            MyPlayData.saveName = SaveName;
            float[] position = new float[3];
            position[0] = 0; //positionX;
            position[1] = 0; //positionY;
            position[2] = 0; //positionZ;
            MyPlayData.position = position;
            MyPlayData.onLevelNum = 2;
            //to remove end
            sMan.SaveSlot(SaveName, 1, new PlayData(SaveName, MyPlayData.health, MyPlayData.position[0], MyPlayData.position[1], MyPlayData.position[2], MyPlayData.onLevelNum));
            sMan.SaveCurrent(SaveName, new PlayData(SaveName, MyPlayData.health, MyPlayData.position[0], MyPlayData.position[1], MyPlayData.position[2], MyPlayData.onLevelNum));
        }
        else if (hidSlotNum.text == "2")
        {
            SaveManager sMan = new SaveManager(); //save game
            //to remove
            MyPlayData.health = 200;
            MyPlayData.saveName = SaveName;
            float[] position = new float[3];
            position[0] = 0; //positionX;
            position[1] = 0; //positionY;
            position[2] = 0; //positionZ;
            MyPlayData.position = position;
            MyPlayData.onLevelNum = 3;
            //to remove end
            sMan.SaveSlot(SaveName, 2, new PlayData(SaveName, MyPlayData.health, MyPlayData.position[0], MyPlayData.position[1], MyPlayData.position[2], MyPlayData.onLevelNum));
            sMan.SaveCurrent(SaveName, new PlayData(SaveName, MyPlayData.health, MyPlayData.position[0], MyPlayData.position[1], MyPlayData.position[2], MyPlayData.onLevelNum));
        }
        else if (hidSlotNum.text == "3")
        {
            SaveManager sMan = new SaveManager(); //save game
            //to remove
            MyPlayData.health = 300;
            MyPlayData.saveName = SaveName;
            float[] position = new float[3];
            position[0] = 0; //positionX;
            position[1] = 0; //positionY;
            position[2] = 0; //positionZ;
            MyPlayData.position = position;
            MyPlayData.onLevelNum = 2;
            //to remove end
            sMan.SaveSlot(SaveName, 3, new PlayData(SaveName, MyPlayData.health, MyPlayData.position[0], MyPlayData.position[1], MyPlayData.position[2], MyPlayData.onLevelNum));
            sMan.SaveCurrent(SaveName, new PlayData(SaveName, MyPlayData.health, MyPlayData.position[0], MyPlayData.position[1], MyPlayData.position[2], MyPlayData.onLevelNum));
        }
    }

}
