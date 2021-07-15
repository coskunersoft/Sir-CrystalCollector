using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Coskunerov.Actors;
using Coskunerov.EventBehaviour;
using Coskunerov.EventBehaviour.Attributes;

/// <summary>
/// Actor script that handles game scene operations 
/// </summary>
public class SceneManagement : GameSingleActor<SceneManagement>
{
    public Transform PlayerPoint;
    public Transform crystalCreationCenter;
    public Vector3 crystalCreationRangeMin;
    public Vector3 crystalCreationRangeMax;
    private int crystalCount;
    public int maxCrystalCount=5;

    
    public void ReplaceLevel()
    {
        FindObjectsOfType<Crystal>().ToList().ForEach(x => ObjectCamp.Instance.TakeObjecy(x));
        PlayerActor.Instance.transform.position = PlayerPoint.position;
        RefleshCrystal();
    }

    private void RefleshCrystal()
    {
        while (crystalCount<maxCrystalCount)
        {
            crystalCount++;
            Crystal crystal=ObjectCamp.Instance.GetObject<Crystal>();
            Vector3 randomPos = crystalCreationCenter.position;
            randomPos.x += Random.Range(-Mathf.Abs(crystalCreationRangeMin.x), Mathf.Abs(crystalCreationRangeMax.x));
            randomPos.z += Random.Range(-Mathf.Abs(crystalCreationRangeMin.z), Mathf.Abs(crystalCreationRangeMax.z));
            crystal.transform.position = randomPos;
        }
    }

    [GE(BaseGameEvents.CrystalCollected)]
    void OnCrystalCollected()
    {
        crystalCount--;
        RefleshCrystal();
    }
    [GE(BaseGameEvents.StartGame)]
    void OnGameStarted()
    {
        crystalCount = 0;
        ReplaceLevel();
    }

}
