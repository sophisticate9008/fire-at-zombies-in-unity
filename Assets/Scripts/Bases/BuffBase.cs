using System;
using R3;
using UnityEngine;

namespace MyBase
{
    public class BuffBase : IBuff
    {
        public BuffBase(string buffName, float duration, GameObject obj)
        {
            BuffName = buffName;
            Duration = duration;
            TheObj = obj;
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
        public void Effect()
        {

        }
        public void Remove()
        {

        }
        public void ApplyAndAutoRemove()
        {
            Effect();
            Observable.Timer(TimeSpan.FromSeconds(Duration))
                .Subscribe(_ => Remove());
        }
        public void EffectControl()
        {
            EnemyBase enemyBase = TheObj.GetComponent<EnemyBase>();
            if (enemyBase.ControlImmunityList.IndexOf(BuffName) == -1)
            {
                enemyBase.CanAction = false;
            }
        }
        public void RemoveControl() {
            EnemyBase enemyBase = TheObj.GetComponent<EnemyBase>();
            enemyBase.CanAction = true;
        }

    }
}