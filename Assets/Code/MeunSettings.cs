using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro; // there is a bug with TMP_Dropdown' has been destroyed but you are still trying to access it.
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MeunSettings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionsDropdown;
    public Dropdown qualityDropdown;
    public Toggle toggleFullScreen;
    public Slider sliderVolume; //main game volume
    public Slider sliderMusic;

    public GameObject OptionsFirstBtnSelection; //the first option that will be select when going to option menu
    public GameObject OptionsExitBtnSelection; //the first option that will be select when exiting the option menu
    public GameObject LoadFirstBtnSelection; //the first option that will be select when going to load menu

    Resolution[] resolutions;

    Settings MyGameSettings;

    public TMP_Text TextBtnLoad1, TextBtnLoad2, TextBtnLoad3;
    public Button BtnLoadOne, BtnLoadTwo, BtnLoadThree;

    private void Start()
    {
        LoadManager lMan = new LoadManager();
        DataGameSettings dataGS = lMan.LoadSettings();

        if (dataGS == null)
        {
            DefaultSettings(true);
        }
        else
        {
            resolutions = Screen.resolutions; //get resolutions

            resolutionsDropdown.ClearOptions();
            List<string> resOptions = new List<string>();

            int currentResolutionsIndex = 0;
            for (int i = 0; i < resolutions.Length; i++) //loop all resolutions
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                resOptions.Add(option);

                if (resolutions[i].width == dataGS.ResolutionsWidth && resolutions[i].height == dataGS.ResolutionsHeight)
                {
                    currentResolutionsIndex = i; //what is my resolutions now from saved data
                }
            }

            resolutionsDropdown.AddOptions(resOptions); //list of resolutions
            resolutionsDropdown.value = currentResolutionsIndex; //set current resolutions
            resolutionsDropdown.RefreshShownValue();

            sliderVolume.value = dataGS.Volume; //set volume in audioMixer UI from saved data

            sliderMusic.value = dataGS.VolumeMusic; //set volume in audioMixer UI from saved data

            qualityDropdown.value = dataGS.QualityIndex; //set quality from saved data

            toggleFullScreen.isOn = dataGS.FullScreen; //set check box FullScreen y/n from saved data

            CreateMyGameSettings(dataGS.Volume, dataGS.VolumeMusic, dataGS.QualityIndex, dataGS.ResolutionsWidth, dataGS.ResolutionsHeight, dataGS.FullScreen);
        }
    }

    private void DefaultSettings(bool SetBack)
    {
        resolutions = Screen.resolutions; //get resolutions

        resolutionsDropdown.ClearOptions();
        List<string> resOptions = new List<string>();

        int currentResolutionsIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) //loop all resolutions
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionsIndex = i; //what is my resolutions now
            }
        }

        if (SetBack) //default setting set back
        {
            Resolution resReSet = resolutions[resOptions.Count - 1];
            Screen.SetResolution(resReSet.width, resReSet.height, true);
            QualitySettings.SetQualityLevel(3);
            audioMixer.SetFloat("MainVolume", 0.75f); audioMixer.SetFloat("MusicVolume", 0.75f); //setting sound to it's index num
            Screen.fullScreen = true;
        }

        resolutionsDropdown.AddOptions(resOptions); //list of resolutions
        resolutionsDropdown.value = currentResolutionsIndex; //set current resolutions
        resolutionsDropdown.RefreshShownValue();

        audioMixer.GetFloat("MainVolume", out float currentVol);
        sliderVolume.value = currentVol; //set current volume in audioMixer UI

        audioMixer.GetFloat("MusicVolume", out float currentMusicVol);
        sliderMusic.value = currentMusicVol; //set current volume in audioMixer UI

        qualityDropdown.value = QualitySettings.GetQualityLevel(); //set current quality

        toggleFullScreen.isOn = Screen.fullScreen; //set current check box FullScreen y/n

        CreateMyGameSettings(currentVol, currentMusicVol, qualityDropdown.value, Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
        SaveMyChange();
    }

    void CreateMyGameSettings(float Vol, float VolMusic, int QualityNum, int ResWidth, int ResHeight, bool FullScr)
    {
        MyGameSettings = new Settings
        {
            GameVolume = Vol,
            GameMusicVolume = VolMusic,
            QualityIndex = QualityNum,
            ResolutionsWidth = ResWidth,
            ResolutionsHeight = ResHeight,
            FullScreen = FullScr
        };
    }

    public void SetResolutions(int resolutionsIndex)
    {
        Resolution res = resolutions[resolutionsIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        if (MyGameSettings != null)
        {
            MyGameSettings.GameResolutionsWidth = res.width;
            MyGameSettings.GameResolutionsHeight = res.height;
            MyGameSettings.GameFullScreen = Screen.fullScreen;
        }
    }

    public void SetVolume(float volume)
    {        
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20); //setting sound to it's index num
        if (MyGameSettings != null)
        {
            MyGameSettings.GameVolume = volume;
        }
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); //setting sound to it's index num
        if (MyGameSettings != null)
        {
            MyGameSettings.GameMusicVolume = volume;
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        if (MyGameSettings != null)
        {
            MyGameSettings.GameQualityIndex = qualityIndex;
        }
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (MyGameSettings != null)
        {
            MyGameSettings.GameFullScreen = isFullScreen;
        }
    }

    private void SaveMyChange()
    {
        SaveManager sMan = new SaveManager(); //save changes
        sMan.SaveSettings(MyGameSettings);
    }

    public void BtnPlay_SetSelection()
    {
        EventSystem.current.SetSelectedGameObject(null);//clear selected object        
        EventSystem.current.SetSelectedGameObject(LoadFirstBtnSelection); //set selected object //the first option that will be select when going to load menu

        BtnLoadOne.interactable = BtnLoadTwo.interactable = BtnLoadThree.interactable = true; //ToDo think on slection when no data e.g., UI
        List<string> ListSlotNames = LoadManager.LoadSavePlayDataSlotNames(); //get save games names
        TextBtnLoad1.text = ListSlotNames[0];
        if (TextBtnLoad1.text == "Empty 1")
            BtnLoadOne.interactable = false;
        TextBtnLoad2.text = ListSlotNames[1];
        if (TextBtnLoad2.text == "Empty 2")
            BtnLoadTwo.interactable = false;
        TextBtnLoad3.text = ListSlotNames[2];
        if (TextBtnLoad3.text == "Empty 3")
            BtnLoadThree.interactable = false;
    }

    public void BtnOptions_SetSelection()
    {
        EventSystem.current.SetSelectedGameObject(null);//clear selected object        
        EventSystem.current.SetSelectedGameObject(OptionsFirstBtnSelection); //set selected object //the first option that will be select when going the option menu
    }

    public void BtnQuit_SetSelection() //label as Back
    {
        SaveMyChange(); //TODO move to save button think about it
        EventSystem.current.SetSelectedGameObject(null);//clear selected object        
        EventSystem.current.SetSelectedGameObject(OptionsExitBtnSelection); //set selected object //the first option that will be select when going the option menu
    }

    public void BtnReSet()
    {
        ReSetManager rMan = new ReSetManager();
        string fred = rMan.ReSetSettings(); //TODO this removes file so will have to create other one ********* do i need this
        
        DefaultSettings(true);

        FindObjectOfType<AudioManager>().PlaySound("clickSound"); //only use this if AS one off called TO SOUND
    }
}
