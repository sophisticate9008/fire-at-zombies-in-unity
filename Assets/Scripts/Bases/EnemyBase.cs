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
        public EnemyConfigBase Config => ConfigManager.Instance.GetConfigByClassName(GetType().Name) as EnemyConfigBase;
        public bool IsInit { get; set; }
        public Queue<IBuff> Buffs { get; } = new();
        public Dictionary<string, IComponent> InstalledComponents { get; } = new();
        public float ControlEndTime { get; set; } = 0f;
        public Dictionary<string, float> BuffEndTimes { get; set; } = new();
        //硬控总结束时间
        public float HardControlEndTime { get; set; }

        public int ImmunityCount { get; set; }
        public int MaxLife { get; set; }
        public int NowLife { get; set; }
        public float EasyHurt { get; set; }

        public virtual void Init()
        {
            NowLife = Config.Life;
            MaxLife = Config.Life;
            ImmunityCount = Config.ImmunityCount;
            IsInit = true;

        }
        protected virtual void Start()
        {
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


        public virtual void Move()
        {
            Vector3 position = transform.position;
            float bottomEdge = -Camera.main.orthographicSize;

            if (position.y > bottomEdge + Config.RangeFire)
            {
                transform.Translate(Config.Speed * Time.deltaTime * Vector3.down);
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

        public void AddBuff(string buffName, GameObject selfObj, float duration)
        {
            //免疫指定控制buff
            if (Config.ControlImmunityList.IndexOf(buffName) != -1)
            {
                return;
            }
            //伤害位置对不上不能上buff
            if(Config.ActionType != "land" && selfObj.GetComponent<ArmChildBase>().Config.DamageType != "all") {
                return;
            }
            if (!BuffEndTimes.ContainsKey(buffName))
            {
                Buffs.Enqueue(BuffFactory.Create(buffName, duration,selfObj, gameObject));
            }
            else
            {
                float endTime = BuffEndTimes[buffName];
                float now = Time.time;
                //buff已经结束
                if (now > endTime)
                {
                    Buffs.Enqueue(BuffFactory.Create(buffName, duration, selfObj, gameObject));
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
