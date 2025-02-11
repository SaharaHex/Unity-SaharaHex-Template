using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager 
{
    public void SaveSettings (Settings s)
    {
        GameSettingsManager.SaveSettings(s);
    }

    public void SaveSlot(string SaveName, int Slot, PlayData p)
    {
        GamePlayDataManager.SavePlayData(SaveName, false, Slot, p);
    }

    public void SaveCurrent(string SaveName, PlayData p)
    {
        GamePlayDataManager.SavePlayData(SaveName, true, 0, p);
    }
}
