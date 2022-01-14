using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : MonoBehaviour
{
    // Add this script to the moveable object
    public RectTransform rectTransform;
    private Vector3 offset;

    public bool isSet =  false; //if decision is made

    [Header("Stats")]
    public float anger;
    public float happiness;
    public float social;

    public bool isRight; //if bla then floats go negative

    private Vector3 startPos;

    public void Start()
    {
        startPos = rectTransform.localPosition;
    }
    public void GetOffset()
    {
        offset = rectTransform.position - Input.mousePosition;
    }

    public void MoveObject()
    {
        rectTransform.position = Input.mousePosition + offset;
        if(rectTransform.localPosition.x >= 200)
        {
            if(isRight)
            {
                isSet = true;
            }
            else
            {
                anger = -anger;
                happiness = -happiness;
                social = -social;

                isSet = true;
            }
        }

        if (rectTransform.localPosition.x <= -200)
        {
            if (!isRight)
            {
                isSet = true;
            }
            else
            {
                anger = -anger;
                happiness = -happiness;
                social = -social;

                isSet = true;
            }
        }
    }

    public void PointerUp()
    {
        if(!isSet)
        {
            rectTransform.localPosition = startPos;
        }
    }
}
