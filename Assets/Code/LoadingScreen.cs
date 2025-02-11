using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private Image _progressBar = null;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsyncOperation()); ///https://www.youtube.com/watch?v=fxxoACKCWVo //how to create an LoadingScreen //TODo more link
    }

    IEnumerator LoadAsyncOperation()
    {
        LoadManager lMan = new LoadManager(); //save game
        DataPlaySettings dataPS = lMan.LoadPlaySettingsCurrent();

        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2); //create an async operation

        while(gameLevel.progress < 1)
        {
            _progressBar.fillAmount = gameLevel.progress; //take the progress bar fill = async opertion progress
            yield return new WaitForEndOfFrame();
        }        
    }

}
