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
            EnemyBase = obj.GetComponent<EnemyBase>();
        }
        public new void Effect()
        {
            originSpeed = EnemyBase.Config.speed;
            EnemyBase.Config.speed = originSpeed * slowRate;
        }

        public new void Remove()
        {
            EnemyBase.Config.speed = originSpeed;
        }
    }
}