using System.Collections.Generic;
using ArmConfigs;
using Factorys;
using MyBase;
using UnityEngine;

public class FissionableComponent : ComponentBase
{


    readonly GameObject prefab;
    public FissionableComponent(string componentName, string type, GameObject selfObj) : base(componentName, type, selfObj)
    {
        Config = selfObj.GetComponent<ArmChildBase>().Config;
        IFissionable FissionableConfig = Config as IFissionable;
        prefab = FissionableConfig.ChildConfig.Prefab;
    }

    public override void TriggerExec(GameObject enemyObj)
    {
        GameObject targetEnemy;
        IArmChild armChildPrefab = prefab.GetComponent<IArmChild>();
        armChildPrefab.FindTargetRandom(enemyObj);
        if (armChildPrefab.TargetEnemy != null)
        {
            targetEnemy = armChildPrefab.TargetEnemy;
        }
        else
        {
            return;
        }

        Vector3 baseDirection = (targetEnemy.transform.position - SelfObj.transform.position).normalized;
        var objs = IMultipleable.MutiInstantiate(prefab, SelfObj.transform.position,  baseDirection);
        
        foreach (var obj in objs)
        {
            obj.GetComponent<ArmChildBase>().FirstExceptQueue.Enqueue(enemyObj);
        }

        IMultipleable.InitObjs(objs);
    }
}
