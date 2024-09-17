using System.Collections;
using System.Collections.Generic;
using Factorys;
using MyBase;
using UnityEngine;
using VContainer;

public class Gun : ArmBase, IMultipleable, IRepeatable
{
    

    public Bullet bulletPrefab;
    private int multipleLevel = 4;
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
    private void Awake() {
         bulletPrefab.GetComponent<Bullet>().InstalledComponents.Add(ComponentFactory.Creat("穿透", null));
         bulletPrefab.GetComponent<Bullet>().InstalledComponents.Add(ComponentFactory.Creat("反弹", null));
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
        Vector3 directionToEnemy = (TargetEnemy.transform.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg; // 计算基础角度
        if (MultipleLevel % 2 == 0)
        {
            baseAngle -= AngleDifference / 2f; // 顺时针偏转一半的角度差
        }

        // 发射 MultipleLevel 数量的子弹
        for (int i = 0; i < MultipleLevel; i++)
        {
            // 计算每个弹道的角度偏移
            float angleOffset = (i - (MultipleLevel - 1) / 2f) * angleDifference;
            float finalAngle = baseAngle + angleOffset;

            // 计算子弹的方向向量（根据最终角度）
            Vector3 bulletDirection = new Vector3(Mathf.Cos(finalAngle * Mathf.Deg2Rad), Mathf.Sin(finalAngle * Mathf.Deg2Rad), 0);

            // 生成子弹，并直接设置方向
            Bullet newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // 不需要设置旋转
            CopyComponents<Bullet>(bulletPrefab, newBullet);
            newBullet.Direction = bulletDirection.normalized; // 子弹的方向向量
            newBullet.Speed = Speed;
            newBullet.Init(); // 初始化子弹
        }
    }

}
