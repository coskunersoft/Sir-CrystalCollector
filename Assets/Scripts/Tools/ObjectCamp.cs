using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.Actors;


/// <summary>
/// A kind of self-expanding pool system  ;)
/// </summary>
public class ObjectCamp : GameSingleActor<ObjectCamp>
{
    private List<Object> allFreeObjects;
    private List<System.Tuple<System.Type, System.Func<Object>>> creationRules;
    private List<System.Tuple<System.Type, System.Action<Object>>> pushActions;
    private List<System.Tuple<System.Type, System.Action<Object>>> takeActions;

    public override void ActorAwake()
    {
        allFreeObjects = new List<Object>();
        creationRules = new List<System.Tuple<System.Type, System.Func<Object>>>();
        takeActions = new List<System.Tuple<System.Type, System.Action<Object>>>();
        pushActions = new List<System.Tuple<System.Type, System.Action<Object>>>();

        
        creationRules.Add(new System.Tuple<System.Type, System.Func<Object>>(typeof(Crystal), () =>
        {
            Crystal Created = Instantiate(GameManager.Instance.generalData.crystalPrefab).GetComponent<Crystal>();
            return Created;
        }));
        pushActions.Add(new System.Tuple<System.Type, System.Action<Object>>(typeof(Crystal), (Object o) =>
        {
            Crystal objectx = o as Crystal;
            objectx.gameObject.SetActive(false);
            objectx.transform.position = Vector3.zero;
        }));
        takeActions.Add(new System.Tuple<System.Type, System.Action<Object>>(typeof(Crystal), (Object o) =>
        {
            Crystal objectx = o as Crystal;
            objectx.gameObject.SetActive(true);
        }));
        
    }
    /// <summary>
    /// Returns an object from the pool simply by specifying its type
    /// </summary>
    public T GetObject<T>() where T : Object
    {
        T result = default;
        System.Type searchingType = typeof(T);
        result = allFreeObjects.Find(x => x.GetType() == searchingType) as T;
        if (!result)
        {
            System.Func<Object> findedRule = creationRules.Find(x => x.Item1 == searchingType)?.Item2;
            if (findedRule != null)
            {
                result = (T)findedRule.Invoke();
            }
        }
        else
        {
            System.Action<Object> findedAction = takeActions.Find(x => x.Item1 == searchingType)?.Item2;
            findedAction?.Invoke(result);
            allFreeObjects.Remove(result);
        }
        return result;
    }
    /// <summary>
    /// Pulls an object back into the pool
    /// </summary>
    public void TakeObjecy<T>(T O) where T : Object
    {
        allFreeObjects.Add(O);
        System.Type searchingType = typeof(T);
        Debug.Log(searchingType);
        System.Action<Object> findedAction = pushActions.Find(x => x.Item1 == searchingType)?.Item2;
        findedAction?.Invoke(O);
    }

    public void TakeAll(System.Type type)
    {
        System.Action<Object> findedAction = pushActions.Find(x => x.Item1 == type)?.Item2;
        allFreeObjects.FindAll(x => x.GetType() == type).ForEach(x => findedAction?.Invoke(x));
    }

}
