using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MenuPause : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject MenuPauseUI;
    public GameObject PausedFirstBtnSelection; //the first option that will be select when going the paused menu
    public GameObject PausedFirstBtnSelectionOnQuit; //the first option that will be select when going the paused Quit menu
    public GameObject SlotBtnSelection; //the first option that will be select when going to save menu
    public GameObject SavingBtnSelection; //the first option that will be select when going to saving menu

    public TMP_InputField TextSaveName; //used when input save name
    public Text hidSaveSlotNum; //which slot click

    public TMP_Text TextBtnSave1, TextBtnSave2, TextBtnSave3;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) ) //|| Input.GetButtonDown("Fire3")) //tocheck and xbox
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        MenuPauseUI.SetActive(false);
        Time.timeScale = 1f; //set game back to normal
        GameIsPaused = false;
        AudioListener.pause = false; //set game back sound to normal
        EventSystem.current.SetSelectedGameObject(null); //clear selected object
    }

    void Pause()
    {
        MenuPauseUI.SetActive(true);
        Time.timeScale = 0f; //stop game
        GameIsPaused = true;
        AudioListener.pause = false; //off game sound (apart from the one that are ignoreListenerPause)
        EventSystem.current.SetSelectedGameObject(null); //clear selected object
        EventSystem.current.SetSelectedGameObject(PausedFirstBtnSelection); //set selected object
        AudioListener.pause = true;
    }

    public void LoadMenu()
    {
        FindObjectOfType<AudioManager>().PlaySound("clickSound"); //only use this if one off called only testing neet better sound

        Time.timeScale = 1f; //set game back to normal
        GameIsPaused = false;
        AudioListener.pause = false; //set game back sound to normal
        EventSystem.current.SetSelectedGameObject(null); //clear selected object
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void BtnQuit_SetSelection()
    {
        EventSystem.current.SetSelectedGameObject(null);//clear selected object        
        EventSystem.current.SetSelectedGameObject(PausedFirstBtnSelectionOnQuit); //the first option that will be select when going the paused Quit menu
    }

    public void BtnQuit_SetSelectionResume()
    {
        EventSystem.current.SetSelectedGameObject(null);//clear selected object        
        EventSystem.current.SetSelectedGameObject(PausedFirstBtnSelection); //the first option that will be select when going back the paused menu
    }

    public void BtnSave_SetSelectionSlot() //main button of Pause Menu
    {
        EventSystem.current.SetSelectedGameObject(null);//clear selected object        
        EventSystem.current.SetSelectedGameObject(SlotBtnSelection); //set selected object //the first option that will be select when going to save menu

        List<string> ListSlotNames = LoadManager.LoadSavePlayDataSlotNames(); //get save games names
        TextBtnSave1.text = ListSlotNames[0];
        TextBtnSave2.text = ListSlotNames[1];
        TextBtnSave3.text = ListSlotNames[2];
    }

    public void BtnSaveOne_SetSelectionAndGetInfo()
    {
        EventSystem.current.SetSelectedGameObject(null);//clear selected object        
        EventSystem.current.SetSelectedGameObject(SavingBtnSelection); //set selected object //the first option that will be select when going to saving menu
        hidSaveSlotNum.text = "1";
        if (TextBtnSave1.text != "Empty 1")
            TextSaveName.text = TextBtnSave1.text;
    }

    public void BtnSaveTwo_SetSelectionAndGetInfo()
    {
        EventSystem.current.SetSelectedGameObject(null);//clear selected object        
        EventSystem.current.SetSelectedGameObject(SavingBtnSelection); //set selected object //the first option that will be select when going to saving menu
        hidSaveSlotNum.text = "2";
        if (TextBtnSave2.text != "Empty 2")
            TextSaveName.text = TextBtnSave2.text;
    }

    public void BtnSaveThree_SetSelectionAndGetInfo()
    {
        EventSystem.current.SetSelectedGameObject(null);//clear selected object        
        EventSystem.current.SetSelectedGameObject(SavingBtnSelection); //set selected object //the first option that will be select when going to saving menu
        if (TextBtnSave3.text != "Empty 3")
            hidSaveSlotNum.text = "3"; TextSaveName.text = TextBtnSave3.text;
    }

    public void BtnLetter(string Letter)
    {
        if (TextSaveName.text.Length != TextSaveName.characterLimit)
            TextSaveName.text = TextSaveName.text + Letter;
    }

    public void BtnDelLetter()
    {
        if (!string.IsNullOrEmpty(TextSaveName.text))
        {
            string _t = TextSaveName.text;
            TextSaveName.text = _t.Remove(_t.Length - 1); 
        }
    }
}
