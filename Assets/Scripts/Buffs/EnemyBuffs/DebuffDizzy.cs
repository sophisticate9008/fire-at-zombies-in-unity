using MyBase;
using UnityEngine;

namespace TheBuffs
{
    public class DebuffDizzy : BuffBase
    {
        private EnemyBase enemyBase;
        public DebuffDizzy(string buffName, float duration, GameObject obj) : base(buffName, duration, obj)
        {
            enemyBase = obj.GetComponent<EnemyBase>();
        }
        public new void Effect()
        {
            EffectControl();
        }

        public new void Remove()
        {
            RemoveControl();
        }
    }
}

