/*
 * Author Mustafa COSKUNER
 * 
 * Coskunersoft@outlook.com
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Coskunerov.EventBehaviour.Attributes;

namespace Coskunerov.EventBehaviour
{
    public abstract class EventBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<T>();
                }
                return instance;
            }
        }
        private List<Data> events = new List<Data>();
        public void PushEvent(int eventID, params object[] data) => events.FindAll(x => x.Id == eventID).ForEach(x => x.Exacute(data));

        /*
        public void PushEvent2(int EventID)
        {
            var monoMembers = FindObjectsOfType<MonoBehaviour>().ToList();
            monoMembers = monoMembers.FindAll(x =>
            {
                System.Type[] types = x.GetType().GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments();
                return types.ToList().Any(ro => ro == GetType());
            });
            foreach (var item in monoMembers)
            {
                var current = item.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance|BindingFlags.Public).Where(m => m.GetCustomAttributes(typeof(GE), true).Length > 0).ToList();
                var filtered = current.FindAll(x => x.GetCustomAttribute<GE>().ID == EventID);
                filtered.ForEach(x => x.Invoke(item,null));
            }

            events.FindAll(x => x.Id == EventID).ForEach(x => x.Exacute());
        }
        */

        public void AddMono(MonoBehaviour mono)
        {
            var current = mono.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Where(m => m.GetCustomAttributes(typeof(GE), true).Length > 0).ToList();
            foreach (var item in current)
            {
                GE gE = item.GetCustomAttribute<GE>();
                Data newx = new Data(gE.ID, mono, item);
                events.Add(newx);
            }
        }
        public void RemoveMono(MonoBehaviour mono)
        {
            events.RemoveAll(x => x == mono);
        }

        public class Data
        {
            public Data(int eventID, MonoBehaviour _behaviour, MethodInfo _methodInfo)
            {
                Id = eventID;
                monoBehaviour = _behaviour;
                methodInfo = _methodInfo;
            }
            private MonoBehaviour monoBehaviour;
            private MethodInfo methodInfo;
            public int Id;
            public void Exacute(params object[] data) => methodInfo.Invoke(monoBehaviour, data);
            public static implicit operator MonoBehaviour(Data data) => data.monoBehaviour;
        }
    }


}



