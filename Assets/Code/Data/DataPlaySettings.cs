using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataPlaySettings
{
    public string saveName;
    public int health;
    public float[] position;
    public int onLevelNum;

    public DataPlaySettings(PlayData p)
    {
        saveName = p.saveName;
        health = p.health;
        position = p.position;
        onLevelNum = p.onLevelNum;
    }
}
