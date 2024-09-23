using MyBase;
using UnityEngine;

namespace TheBuffs
{
    public class DebuffDizzy : BuffBase
    {
        public DebuffDizzy(string buffName, float duration, GameObject obj) : base(buffName, duration, obj)
        {
            EnemyBase = obj.GetComponent<EnemyBase>();
        }
        public new void Effect()
        {
            EffectControl();
            base.Effect();
        }

        public new void Remove()
        {
            RemoveControl();
            base.Remove();
            
        }
    }
}

