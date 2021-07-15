using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, ITriggerTouchListener
{
    

    public void ListenTriggerTouch(MonoBehaviour toucher)
    {
        if(toucher is PlayerActor)
        {
            ((PlayerActor)toucher).TouchCrystal(this);
        } 
    }
}
