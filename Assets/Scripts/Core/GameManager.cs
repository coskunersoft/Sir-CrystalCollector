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
