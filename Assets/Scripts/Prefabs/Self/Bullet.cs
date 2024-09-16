using System;
using System.Collections.Generic;
using MyBase;
using MyComponents;
using UnityEngine;
using VContainer;

public class Bullet : ArmChildBase, IPrefabs
{
    public float speed;        // 子弹速度
    public Vector2 direction;

    public override void Init()
    {

        IsInit = true;
    }

    private void Update()
    {
        if (IsInit)
        {
            transform.Translate(speed * Time.deltaTime * direction);
            // 超出屏幕范围时销毁
            if (IsOutOfBounds())
            {
                Destroy(gameObject);
            }
        }
    }
}

