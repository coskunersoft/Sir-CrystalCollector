using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coskunerov.EventBehaviour
{
    public abstract partial class BaseGameEvents
    {
        public const int GameLoaded = 0;
        public const int StartGame = 1;
        public const int FinishGame = 2;
        public const int CrystalCollected = 3;
        public const int RestartGame = 4;
        public const int ScoreChanged = 5;
    }
}