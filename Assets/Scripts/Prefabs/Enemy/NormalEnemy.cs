

using MyBase;
using UnityEngine;

public class NormalEnemy : EnemyBase
{
    private void Start()
    {
        // 假设该敌人有一个 BoxCollider
        ColliderVolume = GetComponent<CircleCollider2D>();
    }
}