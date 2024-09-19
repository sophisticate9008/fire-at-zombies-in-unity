
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MyBase
{
    public class EnemyBase : MonoBehaviour, IEnemy, IPrefabs
    {
        private IEnemyConfig config;
        public readonly string pathPublic = "Configs/EnemyConfigs";
        public IEnemyConfig Config
        {
            get { return config; }
            set { config = value; }
        }
        private bool isInit;
        public bool IsInit
        {
            get => isInit;
            set
            {
                isInit = value;
            }
        }
        private readonly List<IBuff> buffs = new();
        public List<IBuff> Buffs { get => buffs; }
        private readonly Dictionary<string, IComponent> components = new();
        public Dictionary<string, IComponent> InstalledComponents => components;

        public virtual void Init()
        {
            LoadConfig();
            IsInit = true;
        }
        protected virtual void Start()
        {
            LoadConfig();
        }
        public void Update()
        {
            Move();
        }
        public void LoadConfig()
        {
            // 获取子类的类名
            string className = GetType().Name;

            // 构造文件路径
            string configFileName = Path.Combine(pathPublic, className);

            // 从 Resources 文件夹加载 JSON 文件
            TextAsset json = Resources.Load<TextAsset>(configFileName);

            if (json != null)
            {
                // 解析 JSON 内容
                IEnemyConfig theConfig = JsonUtility.FromJson<EnemyConfig>(json.text);

                // 使用配置初始化字段
                Config = theConfig;
            }
            else
            {
                Debug.LogError($"Config file not found: {configFileName}");
            }
        }

        public virtual void Move()
        {

            Vector3 position = gameObject.transform.position;
            Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
            // 计算下边缘的Y值
            float bottomEdge = -Camera.main.orthographicSize;

            // 检查物体是否在屏幕范围内
            if (position.y > bottomEdge + Config.RangeFire)
            {
                // 物体在范围内时移动
                gameObject.transform.Translate(Config.Speed * Time.deltaTime * new Vector3(0, -1, 0));
            }
            else
            {
                // 物体超出范围时停止移动
            }
        }

        public virtual void Attack()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Skill()
        {
            throw new System.NotImplementedException();
        }

        public virtual void CalLife()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Die()
        {
            TriggerByType("die", gameObject);
        }
        void TriggerByType(string type, GameObject obj)
        {
            foreach (var component in InstalledComponents)
            {
                if (component.Value.Type == type)
                {
                    component.Value.TriggerExec(null);
                }
            }
        }
    }

}




