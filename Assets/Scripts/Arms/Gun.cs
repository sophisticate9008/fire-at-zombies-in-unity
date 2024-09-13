using System.Collections;
using System.Collections.Generic;
using MyBase;
using UnityEngine;
using VContainer;

public class Gun : MonoBehaviour, IMultipleable, IRepeatable, IArms
{

    private int rangeFire = 10;
    private Bullet bulletPrefab;
    private int multipleLevel;
    private int repeatLevel;
    private int fissionLevel;
    private float speed;
    //锁定的敌人
    private Transform targetEnemy;
    public Transform TargetEnemy
    {
        get { return targetEnemy; }
        set { targetEnemy = value; }
    }
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
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

    public int RangeFire
    {
        get => rangeFire;
        set
        {
            rangeFire = value;
        }
    }
    [Inject]
    public void Inject(Bullet bulletPrefab)
    {
        this.bulletPrefab = bulletPrefab;
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        FindTarget(); // 找到最近的敌人
        if (TargetEnemy != null)
        {
            // ShootAtTarget();
        }
    }
    private void FindTarget()
    {
        EnemyBase[] enemies = GameObject.FindObjectsOfType<EnemyBase>();
        float shortestDistance = Mathf.Infinity;
        EnemyBase nearestEnemy = null;

        foreach (EnemyBase enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= RangeFire)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            TargetEnemy = nearestEnemy.transform;
        }
        else
        {
            TargetEnemy = null;
        }
    }
    // 发射子弹
}
