using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    public float Volume, VolumeMusic;
    public int QualityIndex, ResolutionsWidth, ResolutionsHeight;
    public bool FullScreen;

    public float GameVolume
    {
        get { return Volume; }
        set { Volume = value; }
    }

    public float GameMusicVolume
    {
        get { return VolumeMusic; }
        set { VolumeMusic = value; }
    }

    public int GameQualityIndex 
    {
        get { return QualityIndex; }
        set { QualityIndex = value; }
    }

    public int GameResolutionsWidth
    {
        get { return ResolutionsWidth; }
        set { ResolutionsWidth = value; }
    }

    public int GameResolutionsHeight
    {
        get { return ResolutionsHeight; }
        set { ResolutionsHeight = value; }
    }

    public bool GameFullScreen
    {
        get { return FullScreen; }
        set { FullScreen = value; }
    }

}
