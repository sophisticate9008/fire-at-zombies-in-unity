using System.Collections.Generic;
using Factorys;
using MyBase;
using UnityEngine;


public class ArmChildBase : MonoBehaviour, IPrefabs, IArmChild
{
    public GameObject TargetEnemy { get; set; }
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
                    component.TriggerExec(collision.gameObject);
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
    public void CopyComponents<T>(T prefab) where T : MonoBehaviour
    {
        IArmChild prefabArmChild = prefab.GetComponent<IArmChild>();
        IArmChild newInstanceArmChild = gameObject.GetComponent<IArmChild>();

        if (prefabArmChild != null && newInstanceArmChild != null)
        {
            // 清空 newInstance 的组件列表，防止冲突
            newInstanceArmChild.InstalledComponents.Clear();

            // 遍历 prefab 的 InstallComponents 列表，并为 newInstance 创建新的组件实例
            foreach (var component in prefabArmChild.InstalledComponents)
            {
                // 假设有一个方式来复制组件，可以通过工厂或者直接实例化
                var newComponent = ComponentFactory.Creat(component.ComponentName, gameObject.gameObject);
                newInstanceArmChild.InstalledComponents.Add(newComponent);
            }
        }
    }
    public void FindTarget(GameObject nowEnemy)
    {
        EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();

        if (enemies.Length > 0)
        {
            // 将所有敌人添加到列表中，并移除当前敌人
            List<EnemyBase> enemyList = new List<EnemyBase>(enemies);
            enemyList.Remove(nowEnemy.GetComponent<EnemyBase>());  // 移除当前敌人

            if (enemyList.Count > 0)
            {
                // 随机选择一个敌人
                int randomIndex = Random.Range(0, enemyList.Count);
                GameObject randomEnemy = enemyList[randomIndex].gameObject;
                TargetEnemy = randomEnemy;
            }
            else
            {
                // 没有其他敌人，设置为null
                TargetEnemy = null;
            }
        }
        else
        {
            // 如果没有找到敌人，设置为null
            TargetEnemy = null;
        }
    }
}