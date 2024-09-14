using MyBase;
using UnityEngine;

namespace TheBuffs
{
    public class DebuffFreeze : BuffBase
    {
        private EnemyBase enemyBase;
        public DebuffFreeze(string buffName, float duration, GameObject obj) : base(buffName, duration, obj)
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