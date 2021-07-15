using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.Actors;
using Coskunerov.EventBehaviour;
using Coskunerov.EventBehaviour.Attributes;

[RequireComponent(typeof(Rigidbody),typeof(InputManager))]
public class PlayerActor : GameSingleActor<PlayerActor>
{
    private InputManager inputManager;
    private Rigidbody rb;

    [Range(0,100)]
    public float movementSpeedHorizontal = 5;
    [Range(0, 100)]
    public float movementSpeedVertical = 5;

    private Vector3 currentRotation;

    #region Mono
    public override void ActorAwake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }
    #endregion

    #region Game

    #region Update Types
    private System.Action UpdateType(int type) =>
    type switch
    {
        -1=>null,
        0 => GamePlayingUpdate,
        _ => throw new System.ArgumentException(message: "invalid Update type value", paramName: nameof(PlayerActor)),
    };
    private void GamePlayingUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }
    #endregion


    #region Controller
    private void MovePlayer()
    {
        rb.velocity = new Vector3(inputManager.Result.x*movementSpeedHorizontal, rb.velocity.y, inputManager.Result.y*movementSpeedVertical);
    }
    private void RotatePlayer()
    {
        if (rb.velocity.magnitude>0)
        {
            float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(0, angle, 0), 0.2f);
        }
    }
    #endregion

    /// <summary>
    /// player touched an crystal
    /// </summary>
    /// <param name="crystal"></param>
    public void TouchCrystal(Crystal crystal)
    {
        ObjectCamp.Instance.TakeObjecy(crystal);
        GameManager.Instance.EarnScore(10);
        Push(BaseGameEvents.CrystalCollected);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ITriggerTouchListener triggerTouchListener))
        {
            triggerTouchListener.ListenTriggerTouch(this);
        }
    }


    #endregion

    #region Event System Functions

    [GE(BaseGameEvents.StartGame)]
    void OnGameStarted()
    {
        fixedUpdateWork = UpdateType(0);
    }
    [GE(BaseGameEvents.FinishGame)]
    void OnGameFinished()
    {
        fixedUpdateWork = UpdateType(-1);
        rb.velocity = Vector3.zero;
    }

    #endregion


}
