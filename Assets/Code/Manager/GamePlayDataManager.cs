using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class GamePlayDataManager
{
    public static void SavePlayData(string SaveName, bool CurrentGame, int slot, PlayData p)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath;

        if (CurrentGame)
        {
            path += Utility.GamePlayDataFlieNameCurrent; //the slot is not need has we are saveing to Current file
        }
        else
        {
            switch (slot)
            {
                case 1:
                    path += Utility.GamePlayDataFlieName1;
                    break;
                case 2:
                    path += Utility.GamePlayDataFlieName2;
                    break;
                case 3:
                    path += Utility.GamePlayDataFlieName3;
                    break;
                default:
                    path += Utility.GamePlayDataFlieName1;
                    break;
            }
        }

        FileStream stream = new FileStream(path, FileMode.Create);

        DataPlaySettings dps = new DataPlaySettings(p)
        {
            saveName = p.saveName,
            health = p.health,
            position = p.position
        };

        formatter.Serialize(stream, dps);
        stream.Close();
    }
   
    public static DataPlaySettings LoadSettings(int slot)
    {
        string path = Application.persistentDataPath;

        switch (slot)
        {
            case 1:
                path += Utility.GamePlayDataFlieName1;
                break;
            case 2:
                path += Utility.GamePlayDataFlieName2;
                break;
            case 3:
                path += Utility.GamePlayDataFlieName3;
                break;
            default:
                path += Utility.GamePlayDataFlieName1;
                break;
        }

        if (File.Exists(path))
        {
            Debug.Log("Load DataPlaySettings Path " + path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataPlaySettings dps = formatter.Deserialize(stream) as DataPlaySettings;                                    
            stream.Close();

            SaveManager sMan = new SaveManager(); //save game to Current copy
            sMan.SaveCurrent(dps.saveName, new PlayData(dps.saveName, dps.health, dps.position[0], dps.position[1], dps.position[2], dps.onLevelNum));

            return dps;
        }
        else //ToDo it should not get here as button are hide see BtnPlay_SetSelection in MeunSettings
        {
            Debug.LogError("no file");
            return null;
        }
    }

    public static DataPlaySettings LoadSettingsCurrent() 
    {
        string path = Application.persistentDataPath + Utility.GamePlayDataFlieNameCurrent;
        if (File.Exists(path))
        {
            Debug.Log("Load DataPlaySettings Path " + path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataPlaySettings dps = formatter.Deserialize(stream) as DataPlaySettings;
            stream.Close();

            return dps;
        }
        else //create new game 
        {
            SaveManager sMan = new SaveManager(); //save game to Current copy e.g., Default copy 
            NewGame_Lv1(sMan);

            DataPlaySettings dps = new DataPlaySettings(NewGame_Paly());

            Debug.LogError("no file");
            return dps;
        }
    }

    private static PlayData NewGame_Paly() //default Level1 play (create new game) //ToDo this can be move to class
    {
        return new PlayData("Current", 1, 0, 0, 0, 2);
    }

    private static void NewGame_Lv1(SaveManager sMan) //default Level1 (create new game) //ToDo this can be move to class
    {
        sMan.SaveCurrent("Current", new PlayData("Current", 1, 0, 0, 0, 2));
    }

    //public static string ReSetSettings() // now in GameSettingsManager
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    string path = Application.persistentDataPath + Utility.GamePlayDataFlieName;
    //    if (File.Exists(path))
    //    {
    //        File.Delete(path);
    //        return "Back to Default Settings";
    //    }
    //    else
    //    {
    //        Debug.LogError("no file");
    //        return "No File to ReSet, Back to Default Settings";
    //    }
    //}

    public static List<string> LoadSavePlayDataSlotNames()
    {
        List<string> SlotNames = new List<string>();
        string path1 = Application.persistentDataPath + Utility.GamePlayDataFlieName1;
        string path2 = Application.persistentDataPath + Utility.GamePlayDataFlieName2;
        string path3 = Application.persistentDataPath + Utility.GamePlayDataFlieName3;
        GetSlotNames(SlotNames, path1, 1); GetSlotNames(SlotNames, path2, 2); GetSlotNames(SlotNames, path3, 3);
        return SlotNames;
    }

    private static void GetSlotNames(List<string> SlotNames, string path, int SlotNum)
    {
        if (File.Exists(path))
        {
            Debug.Log("Load DataPlaySettings Path " + path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataPlaySettings output = formatter.Deserialize(stream) as DataPlaySettings;
            stream.Close();
            
            SlotNames.Add(output.saveName);
        }
        else
        {
            SlotNames.Add("Empty " + SlotNum.ToString());
            Debug.Log("no file");
        }
    }
}
