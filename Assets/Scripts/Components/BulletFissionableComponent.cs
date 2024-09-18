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
        IMultipleable.MutiInstantiate(bulletPrefab.gameObject, SelfObj.transform.position, SelfObj.GetComponent<ArmChildBase>().Speed, baseDirection, MultipleLevel, angleDifference);
        //第二弹道索敌
        // for (int i = 0; i < MultipleLevel; i++)
        // {
        //     // 计算每个弹道的角度偏移
        //     Vector3 bulletDirection = IMultipleable.CalDirectionDifference(baseDirection, i, MultipleLevel, angleDifference);
        //     // 生成子弹，并根据发射方向设置旋转
        //     ArmChildBase newBullet = GameObject.Instantiate(bulletPrefab, SelfObj.transform.position, Quaternion.identity);
        //     newBullet.CopyComponents<ArmChildBase>(bulletPrefab);
        //     // 子弹的方向和速度设置
        //     newBullet.Direction = bulletDirection.normalized;
        //     newBullet.Speed = SelfObj.GetComponent<ArmChildBase>().Speed;
        //     newBullet.Init(); // 初始化子弹
        // }
    }
}