using System;
using MyComponents;
using UnityEngine;
using VContainer;

public class Bullet : MonoBehaviour, IPrefabs, ISelf
{
    public float speed;        // 子弹速度
    public Vector2 direction;

    private bool isInit;
    private PenetrableComponent penetrableComponent;

    public PenetrableComponent ThePenetrableComponent
    {
        get => penetrableComponent;
        set
        {
            penetrableComponent = value;
            penetrableComponent.TheGameObject = gameObject;
        }
    }

    public bool IsInit
    {
        get => isInit;
        set
        {
            isInit = value;
        }
    }

    private void ReducePenetrationLevel()
    {
        if (ThePenetrableComponent != null)
        {
            ThePenetrableComponent.PenetrationLevel -= 1;
        }

    }


    public void Init()
    {
        ThePenetrableComponent = new PenetrableComponent
        {
            PenetrationLevel = 2
        };
        IsInit = true;
    }
    private bool IsOutOfBounds()
    {
        // 获取子弹在屏幕上的位置
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // 如果子弹超出屏幕边界，返回 true
        return viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ISelf self = collision.GetComponent<ISelf>();
        if (self != null)
        {
            return; // 如果碰撞的是 自家 类型对象，直接返回，不做任何处理
        }
        // 减少穿透层数
        ReducePenetrationLevel();
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

