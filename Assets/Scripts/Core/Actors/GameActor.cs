using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.EventBehaviour;

namespace Coskunerov.Actors
{
    public abstract class GameActor<T2> : Actor where T2 : EventBehaviour<T2>
    {
       private static T2 eventdriver;

        public System.Action updateWork;
        public System.Action fixedUpdateWork;

        protected override sealed void Awake()
        {
            if(!eventdriver) eventdriver = FindObjectOfType<T2>();
            eventdriver.AddMono(this);
            ActorAwake();
        }
        protected override sealed void Start()
        {
            ActorStart();
        }
        protected override sealed void Update()
        {
            updateWork?.Invoke();
            ActorUpdate();
        }
        protected override sealed void FixedUpdate()
        {
            fixedUpdateWork?.Invoke();
            ActorFixedUpdate();
        }
        protected override sealed void OnDestroy()
        {
            eventdriver?.RemoveMono(this);
            ActorOnDestroy();
        }

        protected void Push(int eventID, params object[] data)=> eventdriver.PushEvent(eventID, data);
        
        
        public virtual void ActorStart() { }
        public virtual void ActorAwake() { }
        public virtual void ActorOnDestroy() { }
        public virtual void ActorUpdate() { }
        public virtual void ActorFixedUpdate() { }
    }
}