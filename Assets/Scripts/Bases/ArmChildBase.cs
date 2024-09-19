using System.Collections.Generic;
using Factorys;
using MyBase;
using UnityEngine;


public class ArmChildBase : MonoBehaviour, IPrefabs, IArmChild
{
    private List<GameObject> firstExceptList = new();
    public GameObject TargetEnemy { get; set; }
    private List<IComponent> installComponents = new();
    public bool IsInit { get; set; }
    public List<IComponent> InstalledComponents { get => installComponents; set => installComponents = value; }
    public float Speed { get; set; }
    public Vector3 Direction { get; set; }
    public Vector3 EulerAngle { get; set; }
    public List<GameObject> FirstExceptList => firstExceptList;
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
            foreach(var obj in FirstExceptList) {

                if(obj == collision.gameObject) {
                    FirstExceptList.Remove(obj);

                    return;
                }
            }
            TriggerByType("enter", collision.gameObject);
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
            TriggerByType("exit", collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsNotSelf(collision))
        {
            TriggerByType("stay", collision.gameObject);
        }
    }
    public void TriggerByType(string type, GameObject obj)
    {
        foreach (var component in InstalledComponents)
        {
            if (component.Type == type)
            {
                component.TriggerExec(obj);
            }
        }
    }
    public void OnTriggerUpdate()
    {
        TriggerByType("update", null);
    }
    public virtual void Update()
    {

        if (IsInit)
        {
            Move();
            OnTriggerUpdate();


        }
    }

    public void Move()
    {
        float z;
        if (Direction.x > 0)
        {
            //以Z轴为坐标 使用向量计算出来角度  
            z = -Vector3.Angle(Vector3.up, Direction);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, Direction);
        }
        transform.eulerAngles = new Vector3(0, 0, z);
        float baseAngle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        float finalAngle = baseAngle - z;
        Vector3 newDirection = new Vector3(Mathf.Cos(finalAngle * Mathf.Deg2Rad), Mathf.Sin(finalAngle * Mathf.Deg2Rad), 0);
        // Rotate();
        // 更新Direction使其符合旋转后的方向
        transform.Translate(Speed * Time.deltaTime * newDirection);

        // 超出屏幕范围时销毁
        if (IsOutOfBounds())
        {
            Destroy(gameObject);
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