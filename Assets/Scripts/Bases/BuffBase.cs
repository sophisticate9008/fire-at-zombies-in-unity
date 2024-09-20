using System;
using R3;
using UnityEngine;

namespace MyBase
{
    public class BuffBase : IBuff
    {
        EnemyBase enemyBase;
        public BuffBase(string buffName, float duration, GameObject obj)
        {
            BuffName = buffName;
            Duration = duration;
            TheObj = obj;
            enemyBase = obj.GetComponent<EnemyBase>();
        }
        private GameObject obj;
        public GameObject TheObj
        {
            get { return obj; }
            set { obj = value; }
        }
        private float duration = 5f;
        private string buffName;
        public string BuffName
        {
            get { return buffName; }
            set { buffName = value; }
        }
        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        public virtual void Effect()
        {
        }
        public virtual void Remove()
        {

        }
        public void ApplyAndAutoRemove()
        {
            Effect();
            enemyBase.Buffs.Remove(this);
            Observable.Timer(TimeSpan.FromSeconds(Duration))
                .Subscribe(_ => Remove());
        }
        public void EffectControl()
        {
            float _elapsedTime = 0f;
            var subscription = Observable.Interval(TimeSpan.FromSeconds(1f / 60f))
                .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(Duration)))
                .Subscribe(_ =>
                {
                    _elapsedTime += 1f / 60f;
                    if (enemyBase.Config.ControlImmunityList.IndexOf(BuffName) == -1)
                    {
                        enemyBase.CanAction = false;
                    }
                });
        }
        public void RemoveControl()
        {

            enemyBase.CanAction = true;
            
        }
    }
}