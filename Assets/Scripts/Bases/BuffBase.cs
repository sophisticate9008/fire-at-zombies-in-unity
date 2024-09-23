using System;
using R3;
using UnityEngine;

namespace MyBase
{
    public class BuffBase : IBuff
    {
        public EnemyBase EnemyBase { get; set; }

        public GameObject TheObj { get; set; }

        public float Duration { get; set; } = 5f;

        public string BuffName { get; set; }

        public BuffBase(string buffName, float duration, GameObject obj)
        {
            BuffName = buffName;
            Duration = duration;
            TheObj = obj;
            EnemyBase = obj.GetComponent<EnemyBase>();
        }
        public virtual void Effect()
        {
        }
        public virtual void Remove()
        {

        }
        public void EffectAndAutoRemove()
        {
            Effect();
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
                    if (EnemyBase.Config.controlImmunityList.IndexOf(BuffName) == -1)
                    {
                        EnemyBase.CanAction = false;

                    }
                });
        }
        public virtual void RemoveControl()
        {

            EnemyBase.CanAction = true;

        }
    }
}