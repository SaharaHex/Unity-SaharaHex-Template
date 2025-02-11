using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager
{
    public DataGameSettings LoadSettings()
    {
        return GameSettingsManager.LoadSettings();
    }

    public static List<string> LoadSavePlayDataSlotNames()
    {
        return GamePlayDataManager.LoadSavePlayDataSlotNames();
    }

    public static DataPlaySettings LoadPlaySettings(int slot)
    {
        return GamePlayDataManager.LoadSettings(slot);
    }

    public DataPlaySettings LoadPlaySettingsCurrent()
    {
        return GamePlayDataManager.LoadSettingsCurrent();
    }
}
