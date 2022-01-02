using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [HideInInspector]
    //public Hint hint;

    public TaskGameMode gameMode;

    public static GameObject card;

    public Rigidbody2D cardBody;

    public AudioClip startSound;

    public delegate void MouseClick();
    public static event MouseClick OnMouseClick;

    public static Vector3 mousePosition;

    private Vector3 offset;
    private Vector2 mouseForce;

    public static GameObject selectedObject;

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (OnMouseClick != null)
            {
                OnMouseClick();

                if (selectedObject)
                {
                    offset = selectedObject.transform.position - mousePosition;
                }
            }
        }

        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }



public void EndTask()
    {
        gameMode.TaskFinished();
    }

    public virtual void StartTask()
    {
    }

}
