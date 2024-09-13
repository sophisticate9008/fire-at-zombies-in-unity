using System;
using MyComponents;
using UnityEngine;
using VContainer;

public class Bullet : MonoBehaviour
{
    private PenetrableComponent _penetrableComponent;
    public PenetrableComponent PenetrableComponent
    {
        get => _penetrableComponent;
        set => _penetrableComponent = value;
    }
    [Inject]
    public void Inject(PenetrableComponent penetrableComponent)
    {
        PenetrableComponent = penetrableComponent;
        penetrableComponent.PenetrationLevel = 10;

    }
    private void Update()
    {
        // 检测按键输入并减少穿透等级
        if (Input.GetKeyDown(KeyCode.Space)) // 这里以空格键为例
        {
            ReducePenetrationLevel();
        }
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
}

