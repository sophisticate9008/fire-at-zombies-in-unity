using System.Collections.Generic;
using UnityEngine;
namespace MyBase
{


    public class ArmConfigBase
    {
        private GameObject prefab;
        // Prefab 属性，允许重写
        public virtual GameObject Prefab
        {
            get
            {
                if (prefab == null)
                {
                    prefab = Resources.Load<GameObject>("Prefabs/Self/" + GetType().Name.Replace("Config", ""));
                    return prefab;
                }
                else
                {
                    return prefab;
                }

            }
        }

        // 属性定义
        public virtual float CritRate { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Level { get; set; }
        public virtual float Tlc { get; set; }  // 倍率
        public virtual float Speed { get; set; }
        public virtual int RangeFire { get; set; }
        public virtual float Cd { get; set; }

        public virtual float AttackCd { get; set; }
        public virtual int AttackCount { get; set; }
        public virtual float LastTime { get; set; }
        public virtual List<string> ComponentStrs { get; set; } = new List<string>();

        // 构造函数
        public ArmConfigBase()
        {
            Init();
        }


        // 初始化方法，可以在派生类中重写
        public virtual void Init()
        {
            // 初始化逻辑可以在子类中进行扩展
        }
    }
}