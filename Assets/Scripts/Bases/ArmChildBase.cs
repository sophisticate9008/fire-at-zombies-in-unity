using System.Collections.Generic;
using UnityEngine;


public abstract class ArmChildBase : MonoBehaviour, IPrefabs, IArmChild
{
    private List<IComponent> installComponents = new();
    public bool IsInit { get; set; }
    public List<IComponent> InstalledComponents { get => installComponents; set => installComponents = value; }

    public abstract void Init();

    public bool IsOutOfBounds()
    {
        // 获取子弹在屏幕上的位置
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // 如果子弹超出屏幕边界，返回 true
        return viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IArmChild self = collision.GetComponent<IArmChild>();
        if (self != null)
        {
            return; // 如果碰撞的是 自家 类型对象，直接返回，不做任何处理
        }
        // 减少穿透层数
        foreach (var component in InstalledComponents)
        {
            if (component.Type == "enter")
            {
                component.TriggerExec(gameObject, collision.gameObject);
            }
        }
    }
}