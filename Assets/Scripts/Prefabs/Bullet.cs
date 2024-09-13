using System;
using MyComponents;
using UnityEngine;
using VContainer;

public class Bullet : MonoBehaviour, IPrefabs
{
    private Collider2D colliderVolume;
    private PenetrableComponent penetrableComponent;
    public PenetrableComponent PenetrableComponent
    {
        get => penetrableComponent;
        set
        {
            penetrableComponent = value;
            penetrableComponent.TheGameObject = gameObject;
        }
    }

    public Collider2D ColliderVolume
    {
        get => colliderVolume;
        set => colliderVolume = value;
    }

    [Inject]
    public void Inject(PenetrableComponent penetrableComponent)
    {
        PenetrableComponent = penetrableComponent;
        penetrableComponent.PenetrationLevel = 10;
    }
    private void ReducePenetrationLevel()
    {
        if (PenetrableComponent != null)
        {
            // 减少穿透等级
            PenetrableComponent.PenetrationLevel -= 1;
            // 可选：打印当前穿透等级以便调试
            Debug.Log("Current Penetration Level: " + PenetrableComponent.PenetrationLevel);
        }
    }
    private void Start()
    {
        ColliderVolume = GetComponent<CircleCollider2D>();
    }
}

