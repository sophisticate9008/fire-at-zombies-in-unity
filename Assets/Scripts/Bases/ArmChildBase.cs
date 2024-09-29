
using System.Collections.Generic;
using System.Linq;
using Factorys;
using MyBase;
using UnityEngine;

namespace MyBase
{


    public class ArmChildBase : MonoBehaviour, IClone, IArmChild
    {
        private float stayTriggerTime;
        public ArmConfigBase Config => ConfigManager.Instance.GetConfigByClassName(GetType().Name) as ArmConfigBase;
        public GlobalConfig GlobalConfig => ConfigManager.Instance.GetConfigByClassName("Global") as GlobalConfig;
        // public Dictionary<string, float> DamageAddition => GlobalConfig.GetDamageAddition();
        public virtual GameObject TargetEnemyByArm { get; set; }
        public virtual GameObject TargetEnemy { get; set; }
        public bool IsInit { get; set; }
        public Dictionary<string, IComponent> InstalledComponents { get; set; } = new();
        private Vector3 direction;
        public Vector3 Direction
        {
            get { return direction; }
            set
            {
                direction = value;
                ChangeRotation();
            }
        }
        public Queue<GameObject> FirstExceptQueue { get; set; } = new();
        private readonly Dictionary<string, Queue<GameObject>> collideObjs = new() {
            {"enter", new()},
            {"stay", new()},
            {"exit", new()}
        };
        public Dictionary<string, Queue<GameObject>> CollideObjs => collideObjs;
        public bool IsOutOfBounds()
        {
            // 获取子弹在屏幕上的位置
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

            // 如果子弹超出屏幕边界，返回 true
            return viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsNotSelf(collision))
            {
                while (FirstExceptQueue.Count > 0)
                {
                    var obj = FirstExceptQueue.Dequeue();
                    if (obj == collision.gameObject)
                    {
                        return;
                    }
                }
                CollideObjs["enter"].Enqueue(collision.gameObject);

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
                CollideObjs["exit"].Enqueue(collision.gameObject);

            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {

            if (IsNotSelf(collision))
            {
                if (Time.time - stayTriggerTime > Config.AttackCd)
                {
                    stayTriggerTime = Time.time;
                    CollideObjs["stay"].Enqueue(collision.gameObject);
                }

            }
        }
        public void TriggerByType(string type, GameObject obj)
        {
            TriggerByTypeCallBack(type);
            foreach (var component in InstalledComponents)
            {

                if (component.Value.Type == type)
                {
                    component.Value.TriggerExec(obj);

                }
            }
        }
        public virtual void TriggerByTypeCallBack(string type)
        {

        }
        private void OnTriggerByQueue()
        {
            TriggerByType("update", null);
            foreach (var temp in collideObjs)
            {

                Queue<GameObject> queue = temp.Value;
                if (queue.Count > 0)
                {
                    var obj = queue.Dequeue();
                    if (Config.TriggerType == temp.Key)
                    {
                        CreateDamage(obj);
                    }

                    TriggerByType(temp.Key, obj);
                }
            }
        }
        public virtual void Update()
        {
            if (IsInit)
            {

                Move();
                OnTriggerByQueue();
            }
        }
        public virtual void CreateDamage(GameObject enemyObj)
        {
            FighteManager.Instance.SelfDamageFilter(enemyObj, gameObject);
        }
        public virtual void Move()
        {

            transform.Translate(Config.Speed * Time.deltaTime, 0, 0);

            // 超出屏幕范围时销毁
            if (IsOutOfBounds())
            {
                ReturnToPool();
            }

        }
        public void ChangeRotation()
        {
            float rotateZ = Mathf.Atan2(Direction.y, Direction.x);
            transform.rotation = Quaternion.Euler(0, 0, rotateZ * Mathf.Rad2Deg);

        }
        public virtual void Init()
        {

            CreateComponents();
            foreach (var component in InstalledComponents)
            {
                component.Value.Init();
            }
            IsInit = true;

        }


        public void FindTargetRandom(GameObject nowEnemy)
        {
            EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();

            if (enemies.Length > 0)
            {
                // 将所有敌人添加到列表中，并移除当前敌人
                List<EnemyBase> enemyList = new List<EnemyBase>(enemies);
                if (nowEnemy != null && nowEnemy.activeSelf)
                {
                    enemyList.Remove(nowEnemy.GetComponent<EnemyBase>());  // 移除当前敌人
                }
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
        public List<GameObject> FindTargetInScope(int num = 1, GameObject expectObj = null)
        {
            if(num == 0) {
                return null;
            }
            Vector3 detectionCenter;
            float scopeRadius = Config.ScopeRadius;

            // 如果 expectObj 不为空，使用其位置作为检测中心，否则使用当前物体的碰撞体中心
            if (expectObj != null && expectObj.activeSelf)
            {
                detectionCenter = expectObj.transform.position;
            }
            else
            {
                Collider2D collider = GetComponent<Collider2D>();
                detectionCenter = collider.bounds.center;
            }

            // 获取范围内的所有碰撞体
            Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(detectionCenter, scopeRadius);

            // 排除 expectObj 本身
            if (expectObj != null)
            {
                collidersInRange = collidersInRange.Where(collider => collider.gameObject != expectObj).ToArray();
            }

            // 筛选出所有包含 EnemyBase 组件的敌人
            List<EnemyBase> enemiesInRange = collidersInRange
                .Select(collider => collider.GetComponent<EnemyBase>())
                .Where(enemy => enemy != null)
                .ToList();

            // 如果没有敌人，则返回空列表
            if (enemiesInRange.Count == 0)
            {
                return new List<GameObject>();
            }

            // 随机选择 num 个敌人
            var randomEnemies = enemiesInRange.OrderBy(e => UnityEngine.Random.value).Take(num).Select(e => e.gameObject).ToList();

            // 如果 num == 1，将唯一敌人设置为 TargetEnemy
            if (num == 1 && randomEnemies.Count > 0)
            {
                TargetEnemy = randomEnemies[0];
            }

            return randomEnemies;
        }

        public void ReturnToPool()
        {

            ObjectPoolManager.Instance.ReturnToPool(GetType().Name + "Pool", gameObject);
        }

        public virtual void CreateComponents()
        {
            foreach (var componentStr in Config.ComponentStrs)
            {
                if (!InstalledComponents.ContainsKey(componentStr))
                {
                    var component = ComponentFactory.Create(componentStr, gameObject);
                    InstalledComponents.Add(component.ComponentName, component);
                }


            }

        }
        public virtual void OnDisable()
        {
            foreach (var temp in collideObjs)
            {
                temp.Value.Clear();
            }
            CancelInvoke();
        }
        public virtual void OnEnable()
        {

            stayTriggerTime = -10;
            Invoke(nameof(ReturnToPool), Config.Duration);
        }
    }
}