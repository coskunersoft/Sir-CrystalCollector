using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.Actors;
using Coskunerov.EventBehaviour;
using Coskunerov.EventBehaviour.Attributes;

public class CameraActor : GameSingleActor<CameraActor>
{
    public Transform target;
    public Vector3 followOfset;


    private System.Action UpdateType(int type) =>
    type switch
    {
        -1 => null,
        0 => GamePlayingUpdate,
        _ => throw new System.ArgumentException(message: "invalid Update type value", paramName: nameof(PlayerActor)),
    };
    private void GamePlayingUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.Slerp(transform.position, target.position + followOfset, 0.07f);
    }

    [GE(BaseGameEvents.StartGame)]
    void OnGameStarted()
    {
        fixedUpdateWork = UpdateType(0);
    }
    [GE(BaseGameEvents.FinishGame)]
    void OnGameFinished()
    {
        fixedUpdateWork = UpdateType(-1);
    }


}
