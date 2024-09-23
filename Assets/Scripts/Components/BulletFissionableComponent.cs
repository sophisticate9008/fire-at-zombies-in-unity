using System.Collections.Generic;
using ArmConfigs;
using Factorys;
using UnityEngine;

public class BulletFissionableComponent : ComponentBase, IFissionable
{
    public float AngleDifference { get; set; } = 15f;
    private BulletFissionConfig concreteConfig;
    public int FissionLevel { get; set; }
    public int MultipleLevel { get; set; }

    private ArmChildBase prefab;

    public BulletFissionableComponent(string componentName, string type, GameObject selfObj) : base(componentName, type, selfObj)
    {
        concreteConfig = PlayerStateManager.Instance.bulletConfig.BulletFissionConfig;
        prefab = concreteConfig.Prefab.GetComponent<ArmChildBase>();
        prefab.InstalledComponents.Add(ComponentFactory.Create("穿透", prefab.gameObject));
        MultipleLevel = concreteConfig.FissionLevel;
    }

    public override void TriggerExec(GameObject enemyObj)
    {
        GameObject targetEnemy = null;
        IArmChild armChildPrefab = prefab.GetComponent<IArmChild>();
        armChildPrefab.FindTarget(enemyObj);
        
        if (armChildPrefab.TargetEnemy != null)
        {
            targetEnemy = armChildPrefab.TargetEnemy;
        }
        else
        {
            return;
        }

        Vector3 baseDirection = (targetEnemy.transform.position - SelfObj.transform.position).normalized;
        var objs = IMultipleable.MutiInstantiate(prefab.gameObject, SelfObj.transform.position, SelfObj.GetComponent<ArmChildBase>().Speed, baseDirection, MultipleLevel, AngleDifference);
        
        foreach (var obj in objs)
        {
            obj.GetComponent<ArmChildBase>().FirstExceptQueue.Enqueue(enemyObj);
        }

        IMultipleable.InitObjs(objs);
    }
}
