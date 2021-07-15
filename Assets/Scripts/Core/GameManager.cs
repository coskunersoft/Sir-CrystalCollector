using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.Resources;
using Coskunerov.EventBehaviour;

public class GameManager : EventBehaviour<GameManager>
{
    public GeneralData generalData;
    public Runtime runtime;

    private void Start() => FirstLoad();

    /// <summary>
    /// Function which the game is installed 
    /// </summary>
    private void FirstLoad()
    {
        PushEvent(BaseGameEvents.GameLoaded);
    }


    public void StartGame()
    {
        runtime.Score = 0;
        PushEvent(BaseGameEvents.ScoreChanged);
        runtime.isGameStarted = true;
        PushEvent(BaseGameEvents.StartGame);
    }

    public void EarnScore(int score)
    {
        runtime.Score += score;
        PushEvent(BaseGameEvents.ScoreChanged);
        if (runtime.Score>=100)
        {
            FinishGame();
        }
    }

    public void FinishGame()
    {
        runtime.isGameStarted = false;
        PushEvent(BaseGameEvents.FinishGame);
    }

    public void RestartGame()
    {
        PushEvent(BaseGameEvents.RestartGame);
        StartGame();
    }
    
    /// <summary>
    /// Runtime values  - include the state of the game
    /// </summary>
    [System.Serializable]
    public struct Runtime
    {
        public bool isGameStarted;
        public int Score;
    }
}
