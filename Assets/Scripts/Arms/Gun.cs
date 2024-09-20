using System.Collections;
using System.Collections.Generic;
using Factorys;
using MyBase;
using UnityEngine;
using VContainer;

public class Gun : ArmBase, IRepeatable, IMultipleable
{


    public Bullet bulletPrefab;
    private int multipleLevel = 2;
    private int repeatLevel = 1;
    private int fissionLevel;
    //锁定的敌人
    private float fireCooldown = 1f; // 射击间隔时间
    private float lastFireTime = 0f;
    private float angleDifference = 5f; // 每条弹道之间的固定角度差
    public float AngleDifference
    {
        get => angleDifference;
        set => angleDifference = value;
    }
    public int MultipleLevel
    {
        get { return multipleLevel; }
        set { multipleLevel = value; }
    }
    public int RepeatLevel
    {
        get { return repeatLevel; }
        set { repeatLevel = value; }
    }
    public int FissionLevel
    {
        get => fissionLevel;
        set
        {
            fissionLevel = value;
        }
    }
    private void Awake()
    {
        List<IComponent> installedComponents = bulletPrefab.GetComponent<Bullet>().InstalledComponents;
        installedComponents.Add(ComponentFactory.Creat("穿透", gameObject));
        installedComponents.Add(ComponentFactory.Creat("反弹", gameObject));
        installedComponents.Add(ComponentFactory.Creat("子弹分裂", gameObject));
        installedComponents.Add(ComponentFactory.Creat("冰冻", gameObject, 2.0f));
    }

    // [Inject]
    // public void Inject(Bullet bulletPrefab)
    // {
    //     Debug.Log("Inject Bullet");
    //     this.bulletPrefab = bulletPrefab;
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
        var objs = IMultipleable.MutiInstantiate(bulletPrefab.gameObject, transform.position, Speed, baseDirection, MultipleLevel, angleDifference);
        IMultipleable.InitObjs(objs);
    }

}
