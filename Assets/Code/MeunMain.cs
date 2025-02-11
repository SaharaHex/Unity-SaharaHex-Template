using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MeunMain : MonoBehaviour
{
    public GameObject MeunFirstBtn; //the first option that will be select

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null); //clear selected object
        EventSystem.current.SetSelectedGameObject(MeunFirstBtn); //set selected object
        Debug.Log("s! " + this.gameObject.name); //TODO just name here
    }

    public void PlayNewGame() //play new game //ToDo test this and need craete load new game do curreny one !!!!
    {
        EventSystem.current.SetSelectedGameObject(null); //clear selected object        

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayGameLoad1() //play game in slot one
    {
        EventSystem.current.SetSelectedGameObject(null); //clear selected object        
        LoadManager.LoadPlaySettings(1);

        LoadManager lMan = new LoadManager();
        DataPlaySettings MyPlayData = lMan.LoadPlaySettingsCurrent(); //if null it will create new game

        SceneManager.LoadScene(MyPlayData.onLevelNum);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayGameLoad2() //play game in slot two
    {
        EventSystem.current.SetSelectedGameObject(null); //clear selected object        
        LoadManager.LoadPlaySettings(2);

        LoadManager lMan = new LoadManager();
        DataPlaySettings MyPlayData = lMan.LoadPlaySettingsCurrent(); //if null it will create new game

        SceneManager.LoadScene(MyPlayData.onLevelNum);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayGameLoad3() //play game in slot three
    {
        EventSystem.current.SetSelectedGameObject(null); //clear selected object        
        LoadManager.LoadPlaySettings(3);

        LoadManager lMan = new LoadManager();
        DataPlaySettings MyPlayData = lMan.LoadPlaySettingsCurrent(); //if null it will create new game

        SceneManager.LoadScene(MyPlayData.onLevelNum);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
