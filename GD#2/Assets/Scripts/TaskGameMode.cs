//using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskGameMode : Task
{
    //[Header("UI")]
    //public UIManager UI;

    public GameObject beginScreen;
    public GameObject endScreen;
    //public GameObject buttons;

    [Header("Tasks")]
    public GameObject taskParent;
    public Task currentTask;
    private List<GameObject> tasks = new List<GameObject>();
    private int taskIndex = 0;//When a task gets done, this gets incremented
    private bool levelFinished = false;
    [HideInInspector]
    public bool tutorialTurn = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip startSceneSound;
    private bool startSoundPlayed = false;
    //[Header("Hint")]
    //public Hint hint;

    void Start()
    {
        PlaySound(startSceneSound);

        tasks.Clear();

        Task[] allChildren = taskParent.GetComponentsInChildren<Task>();
        foreach (Task child in allChildren)
        {
            tasks.Add(child.gameObject);
            child.gameMode = this;
            child.gameObject.SetActive(false);
        }
        currentTask = tasks[0].GetComponent<Task>();

        if (beginScreen)
        {
            beginScreen.SetActive(true);
        }

    }

    private void Update()
    {
        //start the game if the start sound has been played and the sound stopped playing
        if (!startSoundPlayed && !audioSource.isPlaying)
        {
            startSoundPlayed = true;
            if (beginScreen)
            {
                beginScreen.SetActive(false);
            }


            //Start the game after the start sound has been played
            //currentTask.gameObject.SetActive(true);
            //currentTask.StartTask();

        }
        if (levelFinished && startSoundPlayed)
        {
            if (!audioSource.isPlaying)
            {
                LevelManager.levelManager.LoadLastLevel();
            }
        }


        currentTask.gameObject.SetActive(true);
        currentTask.StartTask();
        //buttons.SetActive(true);
            
        

    }

    public void StartGame()
    {
        //MusicManager.musicManager.StopMusic();

        foreach (GameObject task in tasks)
        {
            task.GetComponent<Task>().gameMode = this;
        }

        //If there is no tasks set in the game mode, there won't be a task at index 0 either. so end the level so the user doesn't get stuck and report an error
        if (tasks[0])
        {
            tasks[0].SetActive(true);

        }
        else if (!tasks[0])
        {
            Debug.LogError("There is no tasks set in the semantic relationships gamemode!");
            EndLevel();
            //give the player a reward

        }

        // if (hint)
        //{
        //   hint.gameObject.SetActive(true);
        //}
    }


    public void TaskFinished()
    {
        GoToNextTask();
    }

    private void GoToNextTask()
    {
        //only need taskindex, else object references dont work
        //disable the previous task
        if (taskIndex + 1 < tasks.Count) //if same panel is used check if its active, if active dont deactivate and activate (breaks sound)
        {
            if (tasks[taskIndex] != tasks[taskIndex + 1] && tasks[taskIndex].activeSelf)
            {
                tasks[taskIndex].SetActive(false);
            }
        }

        //increment task index
        taskIndex++;
        //enable next task if it exists
        if (taskIndex < tasks.Count)
        {
            if (!tasks[taskIndex].activeSelf)
            {
                tasks[taskIndex].SetActive(true);
                tasks[taskIndex].gameObject.GetComponent<Task>().StartTask();
                currentTask = tasks[taskIndex].GetComponent<Task>();
            }
        }

        //If the last task, then the level is done
        else if (taskIndex >= tasks.Count)
        {
            EndLevel();
        }
    }

    public void BackToTutorial()
    {
        GoBackToTutorial();
    }

    private void GoBackToTutorial()
    {
        if (taskIndex - 1 < tasks.Count) //if same panel is used check if its active, if active dont deactivate and activate (breaks sound)
        {
            if (tasks[taskIndex] != tasks[taskIndex - 1] && tasks[taskIndex].activeSelf)
            {
                tasks[taskIndex].SetActive(false);
            }
        }

        //disable last task and enable tutorial
        currentTask.gameObject.SetActive(false);
    }

    public void ReturnToLastTask()
    {
        currentTask.gameObject.SetActive(true);
        PlaySound(currentTask.startSound);
    }

    /// <summary>
    /// When all the tasks in a level is done call this function. when set up properly it will open an endscreen with a button to go back to overworld
    /// </summary>
    private void EndLevel()
    {

        foreach (GameObject task in tasks)
        {
            task.SetActive(false);
        }


        //UI Stuff
        if (!endScreen)
        {
            Debug.LogError("No endscreen set in Gamemode. Won't be displayed!");
            return;
        }

        //if (!UI)
        //{
        //    Debug.LogError("UI Manager is not set in Gamemode and cant display the endscreen!");
        //    //LevelManager.levelManager.LoadLastLevel();
        //    return;
        //}

        //else if (UI && endScreen)
        //{
        //    //UI.OpenCanvas(endScreen.GetComponent<Canvas>());
        //    //LevelManager.levelManager.LoadLastLevel();

        //}
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.Stop();
        audioSource.clip = audio;
        audioSource.Play();
    }
}
