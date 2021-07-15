using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 firstpos;
    private float mindistance;
    private float maxdistance;

    [HideInInspector]
    private Vector2 lastpos;

    public Vector2 Result
    {
        get { return result; }
    }
    private Vector2 result;


    private void Awake()
    {
        mindistance = ((Screen.width + Screen.height) / 2)*0.01f;
        maxdistance = mindistance*5;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstpos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            Vector2 deltapos = (Vector2)Input.mousePosition - lastpos;
            float dist = Vector2.Distance(Input.mousePosition, firstpos);
            if(dist>mindistance)
            {
                float x =Mathf.Clamp(Input.mousePosition.x - firstpos.x,-maxdistance,maxdistance);
                float y = Mathf.Clamp(Input.mousePosition.y - firstpos.y,-maxdistance,maxdistance);

                result.x = x / maxdistance;
                result.y = y / maxdistance;
            }
            if (dist>maxdistance)
            {
                firstpos += deltapos;
            }
            lastpos = Input.mousePosition;

        }
        if(Input.GetMouseButtonUp(0))
        {
            result = Vector2.zero;
        }
    }
  
}
