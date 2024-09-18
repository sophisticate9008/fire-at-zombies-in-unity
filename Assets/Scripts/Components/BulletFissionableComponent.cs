using System.Collections.Generic;
using Factorys;
using UnityEngine;

public class BulletFissionableComponent : ComponentBase, IFissionable, IMultipleable
{

    private float angleDifference = 10f;
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
        Vector3 directionToEnemy = (TargetEnemy.transform.position - SelfObj.transform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
        //第二弹道索敌
        baseAngle -= AngleDifference / 2f;
        for (int i = 0; i < MultipleLevel; i++)
        {
            // 计算每个弹道的角度偏移
            float angleOffset = (i - (MultipleLevel - 1) / 2f) * AngleDifference;
            float finalAngle = baseAngle + angleOffset;

            // 计算子弹的方向向量（根据最终角度）
            Vector3 bulletDirection = new Vector3(Mathf.Cos(finalAngle * Mathf.Deg2Rad), Mathf.Sin(finalAngle * Mathf.Deg2Rad), 0);

            // 生成子弹，并根据发射方向设置旋转
            ArmChildBase newBullet = GameObject.Instantiate(bulletPrefab, SelfObj.transform.position, Quaternion.identity);
            newBullet.CopyComponents<ArmChildBase>(bulletPrefab);


            // 子弹的方向和速度设置
            newBullet.Direction = bulletDirection.normalized;
            newBullet.Speed = SelfObj.GetComponent<ArmChildBase>().Speed;
            newBullet.Init(); // 初始化子弹
        }
    }
}