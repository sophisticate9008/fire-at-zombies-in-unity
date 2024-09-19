
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MyBase
{
    public class EnemyBase : MonoBehaviour, IEnemy, IPrefabs
    {
        private IEnemyConfig config;
        public readonly string pathPublic = "Configs/EnemyConfigs";
        public IEnemyConfig Config {
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
        public void Init()
        {
            LoadConfig();
            IsInit = true;
        }
        protected virtual void Start()
        {
            LoadConfig();
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

    }

}




