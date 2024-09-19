using System.Collections.Generic;
using Factorys;
using UnityEngine;

public class BulletFissionableComponent : ComponentBase, IFissionable
{

    private float angleDifference = 15f;
    public float AngleDifference
    {
        get => angleDifference; set => angleDifference = value;
    }

    private int bulletFissionCount = PlayerStateManager.Instance.bulletFissionCount;


    ArmChildBase bulletPrefab = Resources.Load<GameObject>("Prefabs/Self/BulletFission").GetComponent<ArmChildBase>();
    public BulletFissionableComponent(string componentName, string type, GameObject selfObj) : base(componentName, type, selfObj)
    {
        bulletPrefab.InstalledComponents.Add(ComponentFactory.Creat("穿透", bulletPrefab.gameObject));
        
    }

    public int FissionLevel { get => bulletFissionCount; set => bulletFissionCount = value; }
    public int MultipleLevel { get => bulletFissionCount; set => bulletFissionCount = value; }

    public override void TriggerExec(GameObject enemyObj)
    {
        GameObject TargetEnemy = null;
        IArmChild armChildPrefab = bulletPrefab.GetComponent<IArmChild>();
        armChildPrefab.FindTarget(enemyObj);
        if (armChildPrefab.TargetEnemy != null)
        {
            TargetEnemy = armChildPrefab.TargetEnemy;
        }
        else
        {
            return;
        }
        Vector3 baseDirection = (TargetEnemy.transform.position - SelfObj.transform.position).normalized;
        var objs = IMultipleable.MutiInstantiate(bulletPrefab.gameObject, SelfObj.transform.position, SelfObj.GetComponent<ArmChildBase>().Speed, baseDirection, MultipleLevel, angleDifference);
        foreach(var obj in objs) {
            obj.GetComponent<ArmChildBase>().FirstExceptList.Add(enemyObj);
        }
        IMultipleable.InitObjs(objs);
    }
}