using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable] //so it's show in Unity as a list
public class PlayData
{
    public string saveName;
    public int health;
    public float[] position;
    public int onLevelNum;

    public PlayData(string SaveName, int Health, float positionX, float positionY, float positionZ, int OnLevelNum)
    {
        saveName = SaveName;
        health = Health;

        position = new float[3];
        position[0] = positionX;
        position[1] = positionY;
        position[2] = positionZ;

        onLevelNum = OnLevelNum;
    }
}
