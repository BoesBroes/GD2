using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image myImage;

    void CheckForClick()
    {
        Vector3 positionToCheck = Task.mousePosition;
        positionToCheck.z = myImage.GetComponent<Image>().sprite.bounds.center.z;

        if (myImage.GetComponent<Image>().sprite.bounds.Contains(positionToCheck))
        {
            Task.card = gameObject;
        }
    }


    void OnEnable()
    {
        Task.OnMouseClick += CheckForClick;
    }

    void OnDisable()
    {
        Task.OnMouseClick -= CheckForClick;
    }
}
