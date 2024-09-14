using MyBase;
using UnityEngine;

namespace TheBuffs
{
    public class DebuffSlow : BuffBase
    {
        private float slowRate;
        private float originSpeed;
        private EnemyBase enemyBase;
        public DebuffSlow(string buffName, float duration, GameObject obj, float slowRate) : base(buffName, duration, obj)
        {
            this.slowRate = slowRate;
            enemyBase = obj.GetComponent<EnemyBase>();
        }
        public new void Effect()
        {
            originSpeed = enemyBase.Speed;
            enemyBase.Speed = originSpeed * slowRate;
        }

        public new void Remove()
        {
            enemyBase.Speed = originSpeed;
        }
    }
}