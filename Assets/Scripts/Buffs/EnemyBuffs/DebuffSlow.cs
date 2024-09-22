using MyBase;
using UnityEngine;

namespace TheBuffs
{
    public class DebuffSlow : BuffBase
    {
        private float slowRate = 0.3f;
        private float originSpeed;

        public DebuffSlow(string buffName, float duration, GameObject obj) : base(buffName, duration, obj)
        {
            enemyBase = obj.GetComponent<EnemyBase>();
        }
        public new void Effect()
        {
            originSpeed = enemyBase.Config.speed;
            enemyBase.Config.speed = originSpeed * slowRate;
        }

        public new void Remove()
        {
            enemyBase.Config.speed = originSpeed;
        }
    }
}