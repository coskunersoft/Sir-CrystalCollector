using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.Actors;

[RequireComponent(typeof(Rigidbody))]
public class PlayerActor : GameSingleActor<PlayerActor>
{

    public void TouchCrystal(Crystal crystal)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITriggerTouchListener triggerTouchListener))
        {
            triggerTouchListener.ListenTriggerTouch(this);
        }
    }
}
