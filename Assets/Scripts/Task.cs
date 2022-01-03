using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [HideInInspector]
    //public Hint hint;

    public TaskGameMode gameMode;

    public GameObject card;

    public AudioClip startSound;


    void Update()
    {
        if (card)
        {
            if (card.GetComponent<MoveCard>().isSet == true)
            {
                EndTask();
            }
        }
    }



    public void EndTask()
    {
        float anger = card.GetComponent<MoveCard>().anger;
        float happiness = card.GetComponent<MoveCard>().happiness;
        float social = card.GetComponent<MoveCard>().social;

        gameMode.TaskFinished(anger, happiness, social);
    }

    public virtual void StartTask()
    {
    }

}
