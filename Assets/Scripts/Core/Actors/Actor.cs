using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coskunerov.Actors
{
    /// <summary>
    /// Base of all of game actors
    /// </summary>
    public abstract class Actor : MonoBehaviour
    {
        protected abstract void Start();
        protected abstract void Awake();
        protected abstract void OnDestroy();
        protected abstract void Update();
        protected abstract void FixedUpdate();
    }
}
