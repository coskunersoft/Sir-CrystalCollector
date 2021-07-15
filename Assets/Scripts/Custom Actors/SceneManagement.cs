using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.Actors;

/// <summary>
/// Actor script that handles game scene operations 
/// </summary>
public class SceneManagement : GameSingleActor<SceneManagement>
{
    public Transform PlayerPoint;
    public Transform crystalCreationCenter;
    public Vector3 crystalCreationRange;
    private int crystalCount;
    public int maxCrystalCount=5;



    public override void ActorStart()
    {
        RefleshCrystal();
    }


    private void RefleshCrystal()
    {
        while (crystalCount<maxCrystalCount)
        {
            crystalCount++;
            Crystal crystal=ObjectCamp.Instance.GetObject<Crystal>();
            crystal.transform.position = crystalCreationCenter.position;
        }
    }
}
