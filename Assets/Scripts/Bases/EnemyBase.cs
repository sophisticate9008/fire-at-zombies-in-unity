using System.Collections.Generic;
using System.IO;
using Factorys;
using UnityEngine;

namespace MyBase
{
    public class EnemyBase : MonoBehaviour, IEnemy, IClone
    {
        public AnimatorManager animatorManager;
        public Animator animator;
        public bool CanAction { get; set; } = true;
        public EnemyConfig Config { get; set; }
        public bool IsInit { get; set; }
        public Queue<IBuff> Buffs { get; } = new();
        public Dictionary<string, IComponent> InstalledComponents { get; } = new();
        public float ControlEndTime { get; set; } = 0f;
        public Dictionary<string, float> BuffEndTimes { get; set; } = new();
        //硬控总结束时间
        public float HardControlEndTime { get; set; }

        private readonly string pathPublic = "Configs/EnemyConfigs";

        public virtual void Init()
        {
            LoadConfig();
            IsInit = true;
        }

        protected virtual void Start()
        {
            LoadConfig();
            animatorManager = AnimatorManager.Instance;
            animator = GetComponent<Animator>();
        }

        public void FixedUpdate()
        {
            if (!CanAction)
            {
                animatorManager.StopAnim(animator);
            }
            else
            {
                animatorManager.PlayAnim(animator, 1f);
            }
        }

        public void Update()
        {
            BuffEffect();
            if (CanAction)
            {
                Move();
            }
        }

        public void LoadConfig()
        {
            string className = GetType().Name;
            string configFileName = Path.Combine(pathPublic, className);
            TextAsset json = Resources.Load<TextAsset>(configFileName);

            if (json != null)
            {
                EnemyConfig theConfig = JsonUtility.FromJson<EnemyConfig>(json.text);
                Config = theConfig;
            }
            else
            {
                Debug.LogError($"Config file not found: {configFileName}");
            }
        }

        public virtual void Move()
        {
            Vector3 position = transform.position;
            float bottomEdge = -Camera.main.orthographicSize;

            if (position.y > bottomEdge + Config.rangeFire)
            {
                transform.Translate(Config.speed * Time.deltaTime * Vector3.down);
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

        public void BuffEffect()
        {
            while (Buffs.Count > 0)
            {
                var buff = Buffs.Dequeue();
                buff.EffectAndAutoRemove();
            }
        }

        public void ReturnToPool()
        {
            throw new System.NotImplementedException();
        }

        public void AddBuff(string buffName, float duration)
        {
            //免疫指定控制buff
            if (Config.controlImmunityList.IndexOf(buffName) != -1)
            {
                return;
            }
            if (!BuffEndTimes.ContainsKey(buffName))
            {
                Buffs.Enqueue(BuffFactory.Create(buffName, duration, gameObject));
            }
            else
            {
                float endTime = BuffEndTimes[buffName];
                float now = Time.time;
                //buff已经结束
                if (now > endTime)
                {
                    Buffs.Enqueue(BuffFactory.Create(buffName, duration, gameObject));
                }
                else
                {
                    //当前+持续时间大于buff结束时间，设置新的结束时间，小于则不用管，被上个buff效果包含了
                    if(now + duration > endTime) {
                        BuffEndTimes[buffName] = now + duration;
                    }
                }
                
            }
        }
    }
}
