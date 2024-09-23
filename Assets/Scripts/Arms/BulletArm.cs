using System.Collections;
using System.Collections.Generic;
using ArmConfigs;
using Factorys;
using MyBase;
using UnityEngine;
using VContainer;

public class BulletArm : ArmBase, IRepeatable, IMultipleable
{


    public Bullet prefab;
    public BulletConfig concreteConfig;
    //锁定的敌人
    private float fireCooldown = 1f; // 射击间隔时间
    private float lastFireTime = 0f;
    private float angleDifference = 5f; // 每条弹道之间的固定角度差
    public float AngleDifference
    {
        get => angleDifference;
        set => angleDifference = value;
    }
    public int MultipleLevel{get;set;}
    public int RepeatLevel{get;set;}
    public int FissionLevel{get;set;}

    protected override void Start()
    {
        base.Start();
        concreteConfig = TheConfig as BulletConfig;
        prefab = concreteConfig.Prefab.GetComponent<Bullet>();
        prefab.InstalledComponents.Add(ComponentFactory.Create("穿透", gameObject));
        prefab.InstalledComponents.Add(ComponentFactory.Create("子弹分裂", gameObject));
        prefab.InstalledComponents.Add(ComponentFactory.Create("反弹", gameObject));
        MultipleLevel = concreteConfig.MultipleLevel;
        RepeatLevel = concreteConfig.RepeatLevel;
        FissionLevel = concreteConfig.BulletFissionCount;
    }

    // [Inject]
    // public void Inject(Bullet prefab)
    // {
    //     Debug.Log("Inject Bullet");
    //     this.prefab = prefab;
    // }


    void Update()
    {
        FindTargetNearest(); // 找到最近的敌人
        if (TargetEnemy != null && Time.time - lastFireTime > fireCooldown)
        {
            StartCoroutine(ShootAtTarget()); // 发射子弹
            lastFireTime = Time.time;
        }
    }
    // 发射子弹
    private IEnumerator ShootAtTarget()
    {
        for (int i = 0; i < RepeatLevel; i++) // 连发逻辑
        {
            ShootMultipleBullets(); // 多条弹道发射
            yield return new WaitForSeconds(0.1f); // 每次连发之间的间隔
        }
    }

    private void ShootMultipleBullets()
    {
        if (TargetEnemy == null) return;

        // 计算从枪口指向敌人的方向向量
        Vector3 baseDirection = (TargetEnemy.transform.position - transform.position).normalized;

        // 发射 MultipleLevel 数量的子弹
        var objs = IMultipleable.MutiInstantiate(prefab.gameObject, transform.position, concreteConfig.Speed, baseDirection, MultipleLevel, angleDifference);
        IMultipleable.InitObjs(objs);
    }

}
