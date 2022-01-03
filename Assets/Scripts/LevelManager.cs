using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    public bool introAnimationPlayed = false;

    //list of disabled scenes
    public List<string> DisabledScenesList = new List<string>();

    //made public to check if scene has loaded before
    public string lastScene;

    // Start is called before the first frame update
    void Awake()
    {
        if (levelManager == null)
        {
            levelManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(levelManager);
            levelManager = this;
            DontDestroyOnLoad(this); //whatever:^)
        }


        //Stops the screen on android/IOS from going to sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }


    public void LoadLastLevel()
    {
        ChangeLevel(lastScene);
    }

    public void ChangeLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
