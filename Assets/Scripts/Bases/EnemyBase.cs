
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MyBase
{
    public class EnemyBase : MonoBehaviour, IEnemy, IPrefabs
    {
        public readonly string pathPublic = "Configs/EnemyConfigs";
        private List<string> controlImmunity;
        private int life = 1000;
        private float speed = 5;
        private int damage = 10;
        private int immunityCount = 0;
        private int blocks = 1;
        private float rangeFire = 5;
        private float atkSpeed = 1;
        private float weight = 10;
        private float derateAd = 0;
        private float derateIce = 0;
        private float derateFire = 0;
        private float derateElec = 0;
        private float derateWind = 0;
        private float derateEnergy = 0;
        private bool canAction;
        private string type;
        private float easyHurt;
        public float EasyHurt
        {
            get => easyHurt;
            set => easyHurt = value;
        }
        public string Type
        {
            get => type;
            set => type = value;
        }
        public List<string> ControlImmunityList
        {
            get => controlImmunity;
            set => controlImmunity = value;
        }
        public bool CanAction
        {
            get => canAction;
            set => canAction = value;
        }
        private bool isInit;
        public int Life
        {
            get => life;
            set => life = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public int Damage
        {
            get => damage;
            set => damage = value;
        }

        public int ImmunityCount
        {
            get => immunityCount;
            set => immunityCount = value;
        }

        public int Blocks
        {
            get => blocks;
            set => blocks = value;
        }

        public float RangeFire
        {
            get => rangeFire;
            set => rangeFire = value;
        }

        public float AtkSpeed
        {
            get => atkSpeed;
            set => atkSpeed = value;
        }

        public float Weight
        {
            get => weight;
            set => weight = value;
        }

        public float DerateAd
        {
            get => derateAd;
            set => derateAd = value;
        }

        public float DerateIce
        {
            get => derateIce;
            set => derateIce = value;
        }

        public float DerateFire
        {
            get => derateFire;
            set => derateFire = value;
        }

        public float DerateElec
        {
            get => derateElec;
            set => derateElec = value;
        }

        public float DerateWind
        {
            get => derateWind;
            set => derateWind = value;
        }

        public float DerateEnergy
        {
            get => derateEnergy;
            set => derateEnergy = value;
        }
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
            IsInit = true;
        }
        protected virtual void Start()
        {
            LoadConfig();
        }

        private void LoadConfig()
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
                EnemyConfig config = JsonUtility.FromJson<EnemyConfig>(json.text);

                // 使用配置初始化字段
                ApplyConfig(config);
            }
            else
            {
                Debug.LogError($"Config file not found: {configFileName}");
            }
        }

        // 在基类中实现 ApplyConfig 方法
        protected void ApplyConfig(EnemyConfig config)
        {
            Life = config.Life;
            Speed = config.Speed;
            Damage = config.Damage;
            ImmunityCount = config.ImmunityCount;
            Blocks = config.Blocks;
            RangeFire = config.RangeFire;
            AtkSpeed = config.AtkSpeed;
            Weight = config.Weight;
            DerateAd = config.DerateAd;
            DerateIce = config.DerateIce;
            DerateFire = config.DerateFire;
            DerateElec = config.DerateElec;
            DerateWind = config.DerateWind;
            DerateEnergy = config.DerateEnergy;
            CanAction = config.CanAction;
            ControlImmunityList = config.ControlImmunityList;
        }
    }

}




