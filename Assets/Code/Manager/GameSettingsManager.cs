using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class GameSettingsManager
{
    public static void SaveSettings(Settings s)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + Utility.GameSettingsFlieName;
        FileStream stream = new FileStream(path, FileMode.Create);

        DataGameSettings dgs = new DataGameSettings(s)
        {
            Volume = s.Volume,
            VolumeMusic = s.VolumeMusic,
            QualityIndex = s.QualityIndex,
            FullScreen = s.FullScreen,
            ResolutionsHeight = s.ResolutionsHeight,
            ResolutionsWidth = s.ResolutionsWidth
        };

        formatter.Serialize(stream, dgs);
        stream.Close();
    }

    public static DataGameSettings LoadSettings ()
    {
        string path = Application.persistentDataPath + Utility.GameSettingsFlieName;
        if (File.Exists(path))
        {
            Debug.Log("Load Settings Path " + path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataGameSettings output = formatter.Deserialize(stream) as DataGameSettings;
            stream.Close();

            return output;
        }
        else
        {
            Debug.LogError("no file");
            return null;
        }
    }

    public static string ReSetSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + Utility.GameSettingsFlieName;
        if (File.Exists(path))
        {
            File.Delete(path);
            return "Back to Default Settings";
        }
        else
        {
            Debug.LogError("no file");
            return "No File to ReSet, Back to Default Settings";
        }
    }
}
