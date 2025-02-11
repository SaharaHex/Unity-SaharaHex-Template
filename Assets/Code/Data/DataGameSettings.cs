using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataGameSettings
{
    public float Volume, VolumeMusic;
    public int QualityIndex, ResolutionsWidth, ResolutionsHeight;
    public bool FullScreen;

    public DataGameSettings (Settings s)
    {
        Volume = s.Volume;
        VolumeMusic = s.VolumeMusic;
        QualityIndex = s.QualityIndex;
        ResolutionsWidth = s.ResolutionsWidth;
        ResolutionsHeight = s.ResolutionsHeight;
        FullScreen = s.FullScreen;
    }
}
