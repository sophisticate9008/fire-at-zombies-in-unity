using System.Collections.Generic;
using UnityEngine;


public class ArmChildBase : MonoBehaviour, IPrefabs, IArmChild
{
    private List<IComponent> installComponents = new();
    public bool IsInit { get; set; }
    public List<IComponent> InstalledComponents { get => installComponents; set => installComponents = value; }
    public float Speed { get; set; }
    public Vector2 Direction { get; set; }

    public bool IsOutOfBounds()
    {
        // 获取子弹在屏幕上的位置
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // 如果子弹超出屏幕边界，返回 true
        return viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsNotSelf(collision))
        {
            foreach (var component in InstalledComponents)
            {
                if (component.Type == "enter")
                {
                    component.TriggerExec(collision.gameObject);
                }
            }
        }
    }
    //排除自身
    private bool IsNotSelf(Collider2D collision)
    {
        IArmChild self = collision.GetComponent<IArmChild>();
        return self == null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsNotSelf(collision))
        {
            foreach (var component in InstalledComponents)
            {
                if (component.Type == "exit")
                {
                    component.TriggerExec(collision.gameObject);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsNotSelf(collision))
        {
            foreach (var component in InstalledComponents)
            {
                if (component.Type == "stay")
                {
                    component.TriggerExec( collision.gameObject);
                }
            }
        }
    }
    public void OnTriggerUpdate()
    {
        foreach (var component in InstalledComponents)
        {
            if (component.Type == "update")
            {
                component.TriggerExec(null);
            }
        }
    }
    public virtual void Update()
    {
        
        if (IsInit)
        {
            OnTriggerUpdate();
            transform.Translate(Speed * Time.deltaTime * Direction);
            // 超出屏幕范围时销毁
            if (IsOutOfBounds())
            {
                Destroy(gameObject);
            }
        }
    }
    public virtual void Init()
    {
        IsInit = true;
    }

}